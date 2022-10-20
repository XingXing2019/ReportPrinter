using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using System.Xml;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelTemplateManagerTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveXmlNode")]
        [TestCase(false, "ReplaceLabelTemplate")]
        [TestCase(false, "AppendXmlNode")]
        [TestCase(false, "ReplaceInnerText")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplateManager\ValidConfig.xml";

            SetupDummyLabelStructureManager("ValidationHeader", "ValidationBody", "ValidationFooter");

            var tempLabelTemplate = "";
            if (!expectedRes)
            {
                if (operation == "RemoveXmlNode")
                    filePath = RemoveXmlNodeOfXmlFile(filePath, "LabelTemplate");
                else if (operation == "ReplaceLabelTemplate")
                {
                    var labelTemplate = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
                    tempLabelTemplate = RemoveAttributeOfXmlFile(labelTemplate, "LabelTemplate", "Id");

                    filePath = ReplaceInnerTextOfXmlFile(filePath, "LabelTemplate", tempLabelTemplate);
                }
                else if (operation == "ReplaceInnerText")
                    filePath = ReplaceInnerTextOfXmlFile(filePath, "LabelTemplate", "WrongPath");
                else if (operation == "AppendXmlNode")
                {
                    var parentName = "LabelTemplateList";
                    var nodeName = "LabelTemplate";
                    var innerText = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
                    filePath = AppendXmlNodeToXmlFile(filePath, parentName, nodeName, innerText);
                }
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;

            try
            {
                var actualRes = LabelTemplateManager.Instance.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var isExist =
                        LabelTemplateManager.Instance.TryGetReportTemplate("DeliveryInfoValidation",
                            out var labelTemplate);
                    Assert.IsTrue(isExist);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                if (!expectedRes)
                {
                    if (!string.IsNullOrEmpty(tempLabelTemplate))
                        File.Delete(tempLabelTemplate);
                    File.Delete(filePath);
                }
            }
        }

        [Test]
        public void Test()
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplateManager\ValidConfig.xml";
            var parentName = "LabelTemplateList";
            var nodeName = "TestNode";
            var innerText = "TestInnerText";
            var attribute = new Dictionary<string, string>
            {
                { "id", "111" },
                { "name", "Xing" }
            };

            filePath = AppendXmlNodeToXmlFile(filePath, parentName, nodeName, innerText, attribute);
            filePath = RemoveXmlNodeOfXmlFile(filePath, nodeName);
        }
        
    }
}