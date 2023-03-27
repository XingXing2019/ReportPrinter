using System;
using NUnit.Framework;
using System.Xml;
using RaphaelLibrary.Code.Init.Label;
using System.Collections.Generic;
using System.IO;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelTemplateTest : TestBase
    {
        [Test]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelTemplate", "Id")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelTemplate", "SavePath")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelTemplate", "FileNameSuffix")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelTemplate", "Timeout")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelFooter", "", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelBody", "", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", false, "LabelHeader", "", true)]
        public void TestReadXml(string filePath, bool expectedRes, string nodeName ="", string attributeName = "", bool resetManager = false)
        {
            SetupDummyLabelStructureManager("ValidationHeader", "ValidationBody", "ValidationFooter");

            if (!expectedRes)
            {
                filePath = TestFileHelper.RemoveAttributeOfXmlFile(filePath, nodeName, attributeName);
            }

            if (resetManager)
            {
                LabelStructureManager.Instance.Reset();
                filePath = TestFileHelper.ReplaceInnerTextOfXmlFile(filePath, nodeName, "");

                if (nodeName == "LabelHeader" || nodeName == "LabelFooter")
                    SetupDummyLabelStructureManager("ValidationBody");
                else if (nodeName == "LabelBody")
                    SetupDummyLabelStructureManager("ValidationHeader", "ValidationFooter");
            }

            var node = TestFileHelper.GetXmlNode(filePath);
            var labelTemplate = new LabelTemplate();

            try
            {
                var actualRes = labelTemplate.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual("DeliveryInfoValidation", labelTemplate.Id);
                    
                    var fileName = GetPrivateField<string>(labelTemplate, "_fileName");
                    Assert.AreEqual("DeliveryInfoValidation", fileName);

                    var savePath = GetPrivateField<string>(labelTemplate, "_savePath");
                    Assert.AreEqual(@".\Result\Label\", savePath);

                    var fileNameSuffix = GetPrivateField<string>(labelTemplate, "_fileNameSuffix");
                    Assert.AreEqual("AccountNumber", fileNameSuffix);

                    var timeout = GetPrivateField<int>(labelTemplate, "_timeout");
                    Assert.AreEqual(10, timeout);


                    var labelStructures = GetPrivateField<List<IStructure>>(labelTemplate, "_labelStructures");
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationHeader", out var header);
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationBody", out var body);
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationFooter", out var footer);

                    AssertHelper.AssertObject(header, labelStructures[0]);
                    AssertHelper.AssertObject(body, labelStructures[1]);
                    AssertHelper.AssertObject(footer, labelStructures[2]);
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

        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
            var node = TestFileHelper.GetXmlNode(filePath);
            var labelTemplate = new LabelTemplate();

            SetupDummyLabelStructureManager("ValidationHeader", "ValidationBody", "ValidationFooter");

            try
            {
                labelTemplate.ReadXml(node);
                var cloned = labelTemplate.Clone();

                AssertHelper.AssertObject(labelTemplate, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}