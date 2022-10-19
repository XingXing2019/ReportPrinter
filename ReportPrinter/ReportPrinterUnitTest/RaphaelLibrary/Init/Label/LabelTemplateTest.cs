using System;
using NUnit.Framework;
using System.Xml;
using RaphaelLibrary.Code.Init.Label;
using System.Collections.Generic;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelTemplateTest : TestBase
    {
        [Test]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_Id.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_SavePath.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_FileNameSuffix.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_Timeout.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_LabelHeader.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_LabelBody.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\InvalidTemplate_LabelFooter.xml", false)]
        public void TestReadXml(string filePath, bool expectedRes)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;
            var labelTemplate = new LabelTemplate();

            SetupDummyLabelStructureManager("ValidationBody");

            if (expectedRes)
            {
                SetupDummyLabelStructureManager("ValidationHeader", "ValidationFooter");
            }

            try
            {
                var actualRes = labelTemplate.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual("DeliveryInfoValidation", labelTemplate.Id);
                    
                    var fileName = GetPrivateField<string>(typeof(LabelTemplate), "_fileName", labelTemplate);
                    Assert.AreEqual("DeliveryInfoValidation", fileName);

                    var savePath = GetPrivateField<string>(typeof(LabelTemplate), "_savePath", labelTemplate);
                    Assert.AreEqual(@".\Result\Label\", savePath);

                    var fileNameSuffix = GetPrivateField<string>(typeof(LabelTemplate), "_fileNameSuffix", labelTemplate);
                    Assert.AreEqual("AccountNumber", fileNameSuffix);

                    var timeout = GetPrivateField<int>(typeof(LabelTemplate), "_timeout", labelTemplate);
                    Assert.AreEqual(10, timeout);


                    var labelStructures = GetPrivateField<List<IStructure>>(typeof(LabelTemplate), "_labelStructures", labelTemplate);
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationHeader", out var header);
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationBody", out var body);
                    LabelStructureManager.Instance.TryGetLabelStructure("ValidationFooter", out var footer);

                    AssertObject(header, labelStructures[0]);
                    AssertObject(body, labelStructures[1]);
                    AssertObject(footer, labelStructures[2]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                LabelStructureManager.Instance.Reset();
            }
        }

        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelTemplate\ValidTemplate.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;
            var labelTemplate = new LabelTemplate();

            SetupDummyLabelStructureManager("ValidationHeader", "ValidationBody", "ValidationFooter");

            try
            {
                labelTemplate.ReadXml(node);
                var cloned = labelTemplate.Clone();

                AssertObject(labelTemplate, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                LabelStructureManager.Instance.Reset();
            }
        }
    }
}