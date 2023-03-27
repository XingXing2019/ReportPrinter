using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ReportPrinterUnitTest.Helper
{
    public class TestFileHelper
    {
        #region Txt

        public string RemoveAttributeOfTxtFile(string filePath, string name)
        {
            var content = File.ReadAllText(filePath);
            var start = content.IndexOf(name);
            var firstQuote = content.IndexOf("\"", start);
            var secondQuote = content.IndexOf("\"", firstQuote + 1);
            var removeContent = content.Substring(start, secondQuote - start + 1);
            content = content.Replace(removeContent, "");

            return CreateTxtFile(filePath, content);
        }

        public string ReplaceAttributeOfTxtFile(string filePath, string name, string value)
        {
            var content = File.ReadAllText(filePath);
            var start = content.IndexOf(name);
            var firstQuote = content.IndexOf("\"", start);
            var secondQuote = content.IndexOf("\"", firstQuote + 1);
            content = content.Substring(0, firstQuote + 1) + value + content.Substring(secondQuote);

            return CreateTxtFile(filePath, content);
        }

        public string AppendLineToTxtFile(string filePath, string line)
        {
            var lines = File.ReadAllLines(filePath).ToList();
            lines.Add(line);
            var content = string.Join('\n', lines);

            return CreateTxtFile(filePath, content);
        }

        #endregion


        #region Xml

        public XmlNode GetXmlNode(string filePath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;

            return node;
        }

        public string RemoveAttributeOfXmlFile(string filePath, string nodeName, string attributeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            RemoveAttributeOfNode(root, nodeName, attributeName);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string ReplaceAttributeOfXmlFile(string filePath, string nodeName, string attributeName, string value)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            ReplaceAttributeOfNode(root, nodeName, attributeName, value);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string ReplaceInnerTextOfXmlFile(string filePath, string nodeName, string innerText)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            ReplaceInnerTextOfNode(root, nodeName, innerText);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string ReplaceInnerTextOfXmlFile(string filePath, string nodeName, string parentNodeName, string innerText)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            ReplaceInnerTextOfNode(root, nodeName, null, parentNodeName, innerText);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string AppendXmlNodeToXmlFile(string filePath, string parentNode, string nodeName, string innerText, Dictionary<string, string> attributes = null)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            AppendXmlNode(xmlDoc, root, parentNode, nodeName, innerText, attributes);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string RemoveXmlNodeOfXmlFile(string filePath, string nodeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            RemoveXmlNode(root, nodeName);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string RemoveXmlNodeOfXmlFile(string filePath, string nodeName, string parentNodeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;
            RemoveXmlNode(root, nodeName, parentNodeName);

            return CreateXmlFile(filePath, xmlDoc);
        }

        public string GetInnerTextOfXmlFile(string filePath, string nodeName, string parentNodeName)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var root = xmlDoc.DocumentElement;

            return GetInnerTextOfNode(root, nodeName, null, parentNodeName);
        }

        #endregion


        #region Helper

        private string CreateTxtFile(string filePath, string content)
        {
            var fileName = Path.GetFileName(filePath);
            var tempPath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllText(tempPath, content);
            return tempPath;
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

        private void ReplaceInnerTextOfNode(XmlNode node, string nodeName, XmlNode parentNode, string parentNodeName, string innerText)
        {
            if (node.Name == nodeName && parentNode != null && parentNode.Name == parentNodeName)
            {
                node.InnerText = innerText;
            }

            if (!node.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                ReplaceInnerTextOfNode(childNode, nodeName, node, parentNodeName, innerText);
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

        private void RemoveXmlNode(XmlNode node, string nodeName, string parentNodeName)
        {
            if (!node.HasChildNodes)
            {
                return;
            }

            if (node.Name == parentNodeName)
            {
                var toBeRemoved = new List<XmlNode>();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name != nodeName) continue;
                    toBeRemoved.Add(childNode);
                }

                toBeRemoved.ForEach(x => node.RemoveChild(x));
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                RemoveXmlNode(childNode, nodeName);
            }
        }

        private string GetInnerTextOfNode(XmlNode node, string nodeName, XmlNode parentNode, string parentNodeName)
        {
            if (node.Name == nodeName && parentNode != null && parentNode.Name == parentNodeName)
            {
                return node.InnerText;
            }

            if (!node.HasChildNodes)
            {
                return string.Empty;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                var innerText = GetInnerTextOfNode(childNode, nodeName, node, parentNodeName);
                if (string.IsNullOrEmpty(innerText))
                    continue;
                return innerText;
            }

            return string.Empty;
        }

        private string CreateXmlFile(string filePath, XmlDocument xmlDoc)
        {
            var fileName = Path.GetFileName(filePath);
            var tempPath = Path.Combine(Path.GetTempPath(), fileName);
            xmlDoc.Save(tempPath);
            return tempPath;
        }

        #endregion
    }
}