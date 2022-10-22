using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelStructureManagerTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false, "Id", false)]
        [TestCase(false, "", true, @"C:\Users\61425\AppData\Local\Temp\InvalidStructure_Sql.txt")]
        [TestCase(false, "", true, @".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\ValidStructure.txt")]
        public void TestReadXml(bool expectedRes, string name = "", bool isReplace = true, string value = "")
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestFile\LabelStructureManager\ValidConfig.xml";

            SetupDummySqlTemplateManager(new Dictionary<string, List<string>>
            {
                {"PrintLabelQuery", new List<string>{ "FullCaseContainer", "SplitCaseContainer"}},
            });

            SetupDummyLabelStructureManager("DeliveryInfoHeader", "DeliveryInfoBody", "DeliveryInfoFooter");

            var tempStructureFile = "";
            try
            {
                if (!expectedRes)
                {
                    filePath = isReplace
                        ? ReplaceInnerTextOfXmlFile(filePath, "LabelStructure", value)
                        : RemoveAttributeOfXmlFile(filePath, "LabelStructure", name);

                    if (isReplace)
                    {
                        var structureFile = @".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Sql.txt";
                        tempStructureFile = RemoveAttributeOfTxtFile(structureFile, "SqlId");
                        SetupDummyLabelStructureManager("TestStructure");
                    }
                }
                
                var node = GetXmlNode(filePath);
                var actualRes = LabelStructureManager.Instance.ReadXml(node);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var labelStructureId = "TestStructure";
                    var isExist = LabelStructureManager.Instance.TryGetLabelStructure(labelStructureId, out var labelStructure);
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
                    if (!string.IsNullOrEmpty(tempStructureFile))
                        File.Delete(tempStructureFile);

                    File.Delete(filePath);
                }
            }
        }

        [Test]
        [TestCase("DeliveryInfoHeader", true)]
        [TestCase("DeliveryInfoFooter", false)]
        public void TestTryGetLabelStructure(string labelStructureId, bool expectedRes)
        {
            SetupDummyLabelStructureManager("DeliveryInfoHeader", "DeliveryInfoBody");

            try
            {
                var actualRes = LabelStructureManager.Instance.TryGetLabelStructure(labelStructureId, out var labelStructure);
                Assert.AreEqual(expectedRes, actualRes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}