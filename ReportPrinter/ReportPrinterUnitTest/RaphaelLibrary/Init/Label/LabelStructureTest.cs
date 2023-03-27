using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelStructureTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\ValidStructure.txt";

        [Test]
        public void TestClone()
        {
            var filePath = S_FILE_PATH;
            var id = "TestId";
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);

            SetupDummySqlTemplateManager(new Dictionary<string, List<string>>
            {
                {"PrintLabelQuery", new List<string>{ "FullCaseContainer", "SplitCaseContainer"}},
            });

            SetupDummyLabelStructureManager("DeliveryInfoHeader", "DeliveryInfoBody", "DeliveryInfoFooter");

            try
            {
                var isSuccess = labelStructure.ReadFile(filePath);
                Assert.IsTrue(isSuccess);

                var cloned = labelStructure.Clone();
                AssertHelper.AssertObject(labelStructure, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [Test]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\ValidStructure.txt", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Reference.txt", false, "StructureId", false, "WrongId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Reference.txt", false, "StructureId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Sql.txt", false, "SqlId", false, "WrongId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Sql.txt", false, "SqlId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Sql.txt", false, "SqlResColumn")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Sql.txt", false, "SqlTemplateId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_SqlVariable.txt", false, "Name")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "SqlId", false, "WrongId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "Comparator")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "ExpectedValue")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "FalseStructure")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "SqlId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "SqlResColumn")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "SqlTemplateId")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "TrueStructure")]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestFile\LabelStructure\InvalidStructure_Validation.txt", false, "Type")]
        public void TestReadFile(string filePath, bool expectedRes, string name = "", bool isRemove = true, string value = "")
        {
            var id = "TestId";
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            var labelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);
            
            SetupDummySqlTemplateManager(new Dictionary<string, List<string>>
            {
                {"PrintLabelQuery", new List<string>{ "FullCaseContainer", "SplitCaseContainer"}},
            });

            SetupDummyLabelStructureManager("DeliveryInfoHeader", "DeliveryInfoBody", "DeliveryInfoFooter");

            try
            {
                if (!expectedRes)
                {
                    filePath = isRemove
                        ? TestFileHelper.RemoveAttributeOfTxtFile(filePath, name)
                        : TestFileHelper.ReplaceAttributeOfTxtFile(filePath, name, value);
                }

                var actualRes = labelStructure.ReadFile(filePath);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var rendererList = GetPrivateField<List<LabelRendererBase>>(labelStructure, "_labelRenderer");
                    Assert.AreEqual(6, rendererList.Count);

                    AssertPlaceHolder<LabelSqlRenderer, SqlPlaceHolder>(rendererList, 0, new Dictionary<string, object>
                    {
                        { "_sql", new Sql { Id = "FullCaseContainer" } },
                        { "_sqlResColumn", new SqlResColumn("DeliveryToName") },
                    });

                    AssertPlaceHolder<LabelTimestampRenderer, TimestampPlaceHolder>(rendererList, 1, new Dictionary<string, object>
                    {
                        { "_isUtc", false },
                        { "_mask", "dd/MM/yyyy" }
                    });

                    AssertPlaceHolder<LabelTimestampRenderer, TimestampPlaceHolder>(rendererList, 2, new Dictionary<string, object>
                    {
                        { "_isUtc", true },
                        { "_mask", "dd-MM-yyyy" }
                    });

                    AssertPlaceHolder<LabelSqlVariableRenderer, SqlVariablePlaceHolder>(rendererList, 3, new Dictionary<string, object>
                    {
                        { "_name", "VariableName" }
                    });
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
        

        #region Helper
        
        private List<PlaceHolderBase> GetPlaceHolder(LabelRendererBase renderer)
        {
            var placeHolder = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");
            return placeHolder;
        }
        
        private void AssertPlaceHolder<TRenderer, TPlaceHolder>(List<LabelRendererBase> rendererList, int index, Dictionary<string, object> expectedValues)
            where TRenderer : LabelRendererBase where TPlaceHolder : PlaceHolderBase
        {
            var renderer = rendererList[index] as TRenderer;
            Assert.IsNotNull(renderer);
            var placeHolders = GetPlaceHolder(renderer);
            Assert.AreEqual(1, placeHolders.Count);
            var placeHolder = placeHolders[0] as TPlaceHolder;
            Assert.IsNotNull(placeHolder);

            foreach (var name in expectedValues.Keys)
            {
                var actualValue = GetPrivateField<object>(placeHolder, name);
                AssertHelper.AssertObject(expectedValues[name], actualValue);
            }
        }

        #endregion
    }
}