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

            var node = GetXmlNode(filePath);

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
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplateManager\ValidConfig.xml";
            var labelStructureIds = new[] { "ValidationHeader", "ValidationBody", "ValidationFooter" };
            SetupDummyLabelStructureManager(labelStructureIds);

            if (!expectedRes)
            {
                filePath = RemoveXmlNodeOfXmlFile(filePath, "LabelTemplate");
            }

            var node = GetXmlNode(filePath);
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

                    var savePath = GetPrivateField<string>(labelTemplate.GetType(), "_savePath", labelTemplate);
                    Assert.AreEqual(@".\Result\Label\", savePath);

                    var fileNameSuffix = GetPrivateField<string>(labelTemplate.GetType(), "_fileNameSuffix", labelTemplate);
                    Assert.AreEqual("AccountNumber", fileNameSuffix);
                    
                    var fileName = GetPrivateField<string>(labelTemplate.GetType(), "_fileName", labelTemplate);
                    Assert.AreEqual("DeliveryInfoValidation", fileName);

                    var timeout = GetPrivateField<int>(labelTemplate.GetType(), "_timeout", labelTemplate);
                    Assert.AreEqual(10, timeout);
                    
                    var labelStructures = GetPrivateField<List<IStructure>>(labelTemplate.GetType(), "_labelStructures", labelTemplate);

                    var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                    for (int i = 0; i < labelStructureIds.Length; i++)
                    {
                        var id = labelStructureIds[i];
                        var expectedLabelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);
                        var prop = expectedLabelStructure.GetType().GetField("_lines", BindingFlags.NonPublic | BindingFlags.Instance);
                        prop?.SetValue(expectedLabelStructure, Array.Empty<string>());

                        AssertObject(expectedLabelStructure, labelStructures[i]);
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