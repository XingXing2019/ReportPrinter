using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.PDF;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase
    {
        protected readonly Dictionary<string, string> ServicePath;

        private readonly Random _random;

        protected TestBase()
        {
            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);

            _random = new Random();
        }

        [TearDown]
        public void TearDown()
        {
            LabelStructureManager.Instance.Reset();
            LabelTemplateManager.Instance.Reset();
            PdfTemplateManager.Instance.Reset();
            SqlTemplateManager.Instance.Reset();
            DatabaseManager.Instance.Reset();
        }

        protected IPrintReport CreateMessage(ReportTypeEnum reportType, bool isValidMessage = true)
        {
            var messageId = Guid.NewGuid();
            var correlationId = Guid.NewGuid();
            var templateId = "Template1";
            var printerId = "Printer1";
            var numberOfCopy = 3;
            var hasReprintFlag = true;

            var index = _random.Next(100);
            var sqlVariables = new List<SqlVariable>
            {
                new SqlVariable { Name = $"Name{index}", Value = $"Value{index}" },
                new SqlVariable { Name = $"Name{index + 1}", Value = $"Value{index + 1}" },
            };

            reportType = isValidMessage
                ? reportType
                : reportType == ReportTypeEnum.Label ? ReportTypeEnum.PDF : ReportTypeEnum.Label;

            var expectedMessage = PrintReportMessageFactory.CreatePrintReportMessage(reportType.ToString());

            expectedMessage.MessageId = messageId;
            expectedMessage.CorrelationId = correlationId;
            expectedMessage.TemplateId = templateId;
            expectedMessage.PrinterId = printerId;
            expectedMessage.NumberOfCopy = numberOfCopy;
            expectedMessage.HasReprintFlag = hasReprintFlag;
            expectedMessage.SqlVariables = sqlVariables;

            return expectedMessage;
        }
        
        protected T GetPrivateField<T>(Type objectType, string fieldName, object instance)
        {
            var fieldInfo = objectType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var field = (T)fieldInfo?.GetValue(instance);

            return field;
        }
        
        #region Database
        
        protected DataTable ListToDataTable<T>(List<T> list, string tableName)
        {
            var dataTable = new DataTable(tableName);

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (var t in list)
            {
                var row = dataTable.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        #endregion
        
        #region Setup

        protected void SetupDummySqlTemplateManager(Dictionary<string, List<string>> sqlDict)
        {
            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(typeof(SqlTemplateManager), "_sqlTemplateList", SqlTemplateManager.Instance);

            foreach (var sqlTemplateId in sqlDict.Keys)
            {
                var sqlIds = sqlDict[sqlTemplateId];
                var sqlTemplate = new SqlTemplate();
                var sqlList = GetPrivateField<Dictionary<string, SqlElementBase>>(typeof(SqlTemplate), "_sqlList", sqlTemplate);

                foreach (var sqlId in sqlIds)
                {
                    sqlList.Add(sqlId, new Sql { Id = sqlId });
                }

                sqlTemplateList.Add(sqlTemplateId, sqlTemplate);
            }
        }

        protected void SetupDummyLabelStructureManager(params string[] labelStructureIds)
        {
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructureList = GetPrivateField<Dictionary<string, IStructure>>(typeof(LabelStructureManager), "_labelStructureList", LabelStructureManager.Instance);

            foreach (var labelStructureId in labelStructureIds)
            {
                var labelStructure = new LabelStructure(labelStructureId, deserializer, LabelElementHelper.LABEL_RENDERER);
                var prop = labelStructure.GetType().GetField("_lines", BindingFlags.NonPublic | BindingFlags.Instance);
                prop?.SetValue(labelStructure, Array.Empty<string>());

                labelStructureList.Add(labelStructureId, labelStructure);
            }
        }

        protected void SetupDummySqlVariableManager(Guid messageId, Dictionary<string, object> sqlVariablesDict)
        {
            var sqlVariables = sqlVariablesDict.ToDictionary(x => x.Key, x => new SqlVariable { Name = x.Key, Value = x.Value.ToString() });
            SqlVariableManager.Instance.StoreSqlVariables(messageId, sqlVariables);
        }

        #endregion
        
        #region Txt

        protected string RemoveAttributeOfTxtFile(string filePath, string name)
        {
            var content = File.ReadAllText(filePath);
            var start = content.IndexOf(name);
            var firstQuote = content.IndexOf("\"", start);
            var secondQuote = content.IndexOf("\"", firstQuote + 1);
            var removeContent = content.Substring(start, secondQuote - start + 1);
            content = content.Replace(removeContent, "");

            return CreateTxtFile(filePath, content);
        }

        protected string ReplaceAttributeOfTxtFile(string filePath, string name, string value)
        {
            var content = File.ReadAllText(filePath);
            var start = content.IndexOf(name);
            var firstQuote = content.IndexOf("\"", start);
            var secondQuote = content.IndexOf("\"", firstQuote + 1);
            content = content.Substring(0, firstQuote + 1) + value + content.Substring(secondQuote);

            return CreateTxtFile(filePath, content);
        }
        
        private string CreateTxtFile(string filePath, string content)
        {
            var fileName = Path.GetFileName(filePath);
            var tempPath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllText(tempPath, content);
            return tempPath;
        }
        
        #endregion
        
        #region Xml

        protected XmlNode GetXmlNode(string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;

            return node;
        }

        protected string RemoveAttributeOfXmlFile(string filePath, string nodeName, string attributeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            RemoveAttributeOfNode(root, nodeName, attributeName);

            return CreateXmlFile(filePath, xmlDoc);
        }

        protected string ReplaceAttributeOfXmlFile(string filePath, string nodeName, string attributeName, string value)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            ReplaceAttributeOfNode(root, nodeName, attributeName, value);

            return CreateXmlFile(filePath, xmlDoc);
        }

        protected string ReplaceInnerTextOfXmlFile(string filePath, string nodeName, string innerText)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            ReplaceInnerTextOfNode(root, nodeName, innerText);

            return CreateXmlFile(filePath, xmlDoc);
        }

        protected string AppendXmlNodeToXmlFile(string filePath, string parentNode, string nodeName, string innerText, Dictionary<string, string> attributes = null)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            AppendXmlNode(xmlDoc, root, parentNode, nodeName, innerText, attributes);

            return CreateXmlFile(filePath, xmlDoc);
        }

        protected string RemoveXmlNodeOfXmlFile(string filePath, string nodeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            RemoveXmlNode(root, nodeName);

            return CreateXmlFile(filePath, xmlDoc);
        }

        private void RemoveAttributeOfNode(XmlNode node, string nodeName, string attributeName)
        {
            if (node.Name == nodeName && node.Attributes != null)
            {
                var toBeRemoved = new List<XmlAttribute>();
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name != attributeName) continue;
                    toBeRemoved.Add(attribute);
                }
                toBeRemoved.ForEach(x => node.Attributes.Remove(x));
            }

            if (!node.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                RemoveAttributeOfNode(childNode, nodeName, attributeName);
            }
        }

        private void ReplaceAttributeOfNode(XmlNode node, string nodeName, string attributeName, string value)
        {
            if (node.Name == nodeName && node.Attributes != null)
            {
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name != attributeName) continue;
                    attribute.Value = value;
                }
            }

            if (!node.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                ReplaceAttributeOfNode(childNode, nodeName, attributeName, value);
            }
        }

        private void ReplaceInnerTextOfNode(XmlNode node, string nodeName, string innerText)
        {
            if (node.Name == nodeName)
            {
                node.InnerXml = innerText;
            }

            if (!node.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                ReplaceInnerTextOfNode(childNode, nodeName, innerText);
            }
        }

        private void AppendXmlNode(XmlDocument xmlDoc, XmlNode node, string parentNode, string nodeName, string innerText, Dictionary<string, string> attributes)
        {
            if (!node.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                AppendXmlNode(xmlDoc, childNode, parentNode, nodeName, innerText, attributes);
            }

            if (node.Name == parentNode)
            {
                var newChild = xmlDoc.CreateElement(nodeName);
                newChild.InnerXml = innerText;

                if (attributes != null)
                {
                    foreach (var key in attributes.Keys)
                    {
                        newChild.SetAttribute(key, attributes[key]);
                    }
                }

                node.AppendChild(newChild);
            }
        }

        private void RemoveXmlNode(XmlNode node, string nodeName)
        {
            if (!node.HasChildNodes)
            {
                return;
            }

            var toBeRemoved = new List<XmlNode>();
            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name != nodeName) continue;
                toBeRemoved.Add(childNode);
            }

            toBeRemoved.ForEach(x => node.RemoveChild(x));

            foreach (XmlNode childNode in node.ChildNodes)
            {
                RemoveXmlNode(childNode, nodeName);
            }
        }
        
        private string CreateXmlFile(string filePath, XmlDocument xmlDoc)
        {
            var fileName = Path.GetFileName(filePath);
            var tempPath = Path.Combine(Path.GetTempPath(), fileName);
            xmlDoc.Save(tempPath);
            return tempPath;
        }

        #endregion

        #region Assert

        protected void AssetMessage(IPrintReport expected, IPrintReport actual)
        {
            Assert.AreEqual(expected.MessageId, actual.MessageId);
            Assert.AreEqual(expected.CorrelationId, actual.CorrelationId);
            Assert.AreEqual(expected.ReportType, actual.ReportType);
            Assert.AreEqual(expected.TemplateId, actual.TemplateId);
            Assert.AreEqual(expected.PrinterId, actual.PrinterId);
            Assert.AreEqual(expected.NumberOfCopy, actual.NumberOfCopy);
            Assert.AreEqual(expected.HasReprintFlag, actual.HasReprintFlag);

            Assert.AreEqual(expected.SqlVariables.Count, actual.SqlVariables.Count);
            foreach (var variable in expected.SqlVariables)
            {
                Assert.IsTrue(actual.SqlVariables.Any(x => x.Name == variable.Name && x.Value == variable.Value));
            }
        }

        protected void AssertObject(object obj1, object obj2)
        {
            try
            {
                Assert.AreEqual(obj1.GetType(), obj2.GetType());
                var type = obj1.GetType();

                if (!type.IsClass || type == typeof(string))
                    Assert.AreEqual(obj1, obj2);
                else
                {
                    var propInfos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    foreach (var prop in propInfos)
                    {
                        var value1 = prop.GetValue(obj1);
                        var value2 = prop.GetValue(obj2);

                        if (value1 == null && value2 == null)
                            continue;
                        if (value1 == null || value2 == null)
                            throw new ApplicationException($"Could not get value of obj1 or obj2");

                        if (prop.PropertyType.IsInterface)
                            AssertObject(value1, value2);
                        else if (!prop.PropertyType.IsClass || prop.PropertyType == typeof(string))
                            Assert.AreEqual(value1, value2);
                        else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                            AssertDictionary(value1, value2);
                        else if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                            AssertList(prop.PropertyType, value1, value2);
                        else if (prop.PropertyType.IsClass)
                            AssertObject(value1, value2);
                        else
                            throw new ApplicationException($"Unknown field type");
                    }

                    var fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).Where(x => !x.Name.Contains("k__BackingField")).ToArray();
                    foreach (var field in fieldInfos)
                    {
                        var value1 = field.GetValue(obj1);
                        var value2 = field.GetValue(obj2);

                        if (value1 == null && value2 == null)
                            continue;
                        if (value1 == null || value2 == null)
                            throw new ApplicationException($"Could not get value of obj1 or obj2");

                        if (field.FieldType.IsInterface)
                            AssertObject(value1, value2);
                        else if (!field.FieldType.IsClass || field.FieldType == typeof(string))
                            Assert.AreEqual(value1, value2);
                        else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                            AssertDictionary(value1, value2);
                        else if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                            AssertList(field.FieldType, value1, value2);
                        else if (field.FieldType.IsClass)
                            AssertObject(value1, value2);
                        else
                            throw new ApplicationException($"Unknown field type");
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        private void AssertDictionary(object value1, object value2)
        {
            var dict1 = (IDictionary)value1;
            var dict2 = (IDictionary)value2;

            Assert.AreEqual(dict1.Count, dict2.Count);
            foreach (var key in dict1.Keys)
            {
                Assert.IsTrue(dict2.Contains(key));
                AssertObject(dict1[key], dict2[key]);
            }
        }

        private void AssertList(Type type, object value1, object value2)
        {
            var itemType = type.GetElementType() ?? type.GenericTypeArguments[0];
            var listType = typeof(List<>).MakeGenericType(itemType);
            var list1 = (IList)Activator.CreateInstance(listType);
            var list2 = (IList)Activator.CreateInstance(listType);

            if (list1 == null || list2 == null)
            {
                throw new ApplicationException($"List of {itemType.Name} is not created");
            }

            foreach (var item in (IEnumerable)value1)
                list1.Add(item);
            foreach (var item in (IEnumerable)value2)
                list2.Add(item);

            Assert.AreEqual(list1.Count, list2.Count);
            for (int i = 0; i < list1.Count; i++)
                AssertObject(list1[i], list2[i]);
        }

        #endregion
    }
}