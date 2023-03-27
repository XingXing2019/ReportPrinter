using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using System.Xml;
using RaphaelLibrary.Code.Render.Label.Helper;
using System.Reflection;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelTemplateManagerTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplateManager\ValidConfig.xml";

        [Test]
        [TestCase(true)]
        [TestCase(false, "RemoveXmlNode")]
        [TestCase(false, "ReplaceLabelTemplate")]
        [TestCase(false, "AppendXmlNode")]
        [TestCase(false, "ReplaceInnerText")]
        public void TestReadXml(bool expectedRes, string operation = "")
        {
            var filePath = S_FILE_PATH;
            SetupDummyLabelStructureManager("ValidationHeader", "ValidationBody", "ValidationFooter");

            var tempLabelTemplate = "";
            if (!expectedRes)
            {
                if (operation == "RemoveXmlNode")
                    filePath = TestFileHelper.RemoveXmlNodeOfXmlFile(filePath, "LabelTemplate");
                else if (operation == "ReplaceLabelTemplate")
                {
                    var labelTemplate = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
                    tempLabelTemplate = TestFileHelper.RemoveAttributeOfXmlFile(labelTemplate, "LabelTemplate", "Id");

                    filePath = TestFileHelper.ReplaceInnerTextOfXmlFile(filePath, "LabelTemplate", tempLabelTemplate);
                }
                else if (operation == "ReplaceInnerText")
                    filePath = TestFileHelper.ReplaceInnerTextOfXmlFile(filePath, "LabelTemplate", "WrongPath");
                else if (operation == "AppendXmlNode")
                {
                    var parentName = "LabelTemplateList";
                    var nodeName = "LabelTemplate";
                    var innerText = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
                    filePath = TestFileHelper.AppendXmlNodeToXmlFile(filePath, parentName, nodeName, innerText);
                }
            }

            var node = TestFileHelper.GetXmlNode(filePath);

            try
            {
                var actualRes = LabelTemplateManager.Instance.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var isExist = LabelTemplateManager.Instance.TryGetReportTemplate("DeliveryInfoValidation", out var labelTemplate);
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
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetReportTemplate(bool expectedRes)
        {
            var filePath = S_FILE_PATH;
            var labelStructureIds = new[] { "ValidationHeader", "ValidationBody", "ValidationFooter" };
            SetupDummyLabelStructureManager(labelStructureIds);

            if (!expectedRes)
            {
                filePath = TestFileHelper.RemoveXmlNodeOfXmlFile(filePath, "LabelTemplate");
            }

            var node = TestFileHelper.GetXmlNode(filePath);
            LabelTemplateManager.Instance.ReadXml(node);

            try
            {
                var actualRes = LabelTemplateManager.Instance.TryGetReportTemplate("DeliveryInfoValidation", out var template);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var labelTemplate = template as LabelTemplate;
                    Assert.IsNotNull(labelTemplate);

                    Assert.AreEqual("DeliveryInfoValidation", labelTemplate.Id);

                    var savePath = GetPrivateField<string>(labelTemplate, "_savePath");
                    Assert.AreEqual(@".\Result\Label\", savePath);

                    var fileNameSuffix = GetPrivateField<string>(labelTemplate, "_fileNameSuffix");
                    Assert.AreEqual("AccountNumber", fileNameSuffix);
                    
                    var fileName = GetPrivateField<string>(labelTemplate, "_fileName");
                    Assert.AreEqual("DeliveryInfoValidation", fileName);

                    var timeout = GetPrivateField<int>(labelTemplate, "_timeout");
                    Assert.AreEqual(10, timeout);
                    
                    var labelStructures = GetPrivateField<List<IStructure>>(labelTemplate, "_labelStructures");

                    var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                    for (int i = 0; i < labelStructureIds.Length; i++)
                    {
                        var id = labelStructureIds[i];
                        var expectedLabelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);
                        var prop = expectedLabelStructure.GetType().GetField("_lines", BindingFlags.NonPublic | BindingFlags.Instance);
                        prop?.SetValue(expectedLabelStructure, Array.Empty<string>());

                        AssertHelper.AssertObject(expectedLabelStructure, labelStructures[i]);
                    }
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
                    File.Delete(filePath);
                }
            }
        }
    }
}