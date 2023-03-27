using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.Helper
{
    public class AssertHelper
    {
        public void AssetMessage(IPrintReport expected, IPrintReport actual)
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

        public void AssertObject(object obj1, object obj2)
        {
            try
            {
                Assert.AreEqual(obj1.GetType(), obj2.GetType());
                var type = obj1.GetType();

                if (!type.IsClass || type == typeof(string))
                    Assert.AreEqual(obj1, obj2);
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    AssertDictionary(obj1, obj2);
                else if (typeof(IEnumerable).IsAssignableFrom(type))
                    AssertList(type, obj1, obj2);
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

        #region Helper
        
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