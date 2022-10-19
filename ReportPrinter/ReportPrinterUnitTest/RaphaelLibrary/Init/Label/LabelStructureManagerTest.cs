using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelStructureManagerTest : TestBase
    {
        [Test]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructureManagerConfig\ValidConfig.xml", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructureManagerConfig\InvalidConfig_Id.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructureManagerConfig\InvalidConfig_InvalidStructure.xml", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructureManagerConfig\InvalidConfig_Duplicate.xml", false)]
        public void TestReadXml(string filePath, bool expectedRes)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.DocumentElement;

            SetupDummySqlTemplateManager(new Dictionary<string, List<string>>
            {
                {"PrintLabelQuery", new List<string>{ "FullCaseContainer", "SplitCaseContainer"}},
            });

            SetupDummyLabelStructureManager("DeliveryInfoHeader", "DeliveryInfoBody", "DeliveryInfoFooter");

            try
            {
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
                SqlTemplateManager.Instance.Reset();
                LabelStructureManager.Instance.Reset();
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
            finally
            {
                LabelStructureManager.Instance.Reset();
            }
        }

    }
}