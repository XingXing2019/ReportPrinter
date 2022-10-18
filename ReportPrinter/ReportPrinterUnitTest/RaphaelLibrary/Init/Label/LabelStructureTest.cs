using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Init.Label
{
    public class LabelStructureTest : TestBase
    {
        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Init\Label\TestTemplate\ValidLabelTemplate.txt";
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
                labelStructure.ReadFile(filePath);
                var cloned = labelStructure.Clone();

                AssertObject(labelStructure, cloned);
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
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\ValidLabelTemplate.txt", true)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Reference_CannotGetStructure.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Reference_StructureId.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Sql_CannotGetSql.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Sql_SqlId.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Sql_SqlResColumn.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Sql_SqlTemplateId.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_SqlVariable_Name.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_CannotGetSql.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_Comparator.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_ExpectedValue.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_FalseStructure.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_SqlId.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_SqlResColumn.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_SqlTemplateId.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_TrueStructure.txt", false)]
        [TestCase(@".\RaphaelLibrary\Init\Label\TestTemplate\InvalidLabelTemplate_Validation_Type.txt", false)]
        public void TestReadFile(string filePath, bool expectedRes)
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
                var actualRes = labelStructure.ReadFile(filePath);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    var rendererList = GetPrivateField<List<LabelRendererBase>>(labelStructure.GetType(), "_labelRenderer", labelStructure);
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
                SqlTemplateManager.Instance.Reset();
                LabelStructureManager.Instance.Reset();
            }
        }
        

        #region Helper

        private List<PlaceHolderBase> GetPlaceHolder(LabelRendererBase renderer)
        {
            var placeHolder = GetPrivateField<List<PlaceHolderBase>>(renderer.GetType(), "PlaceHolders", renderer);
            return placeHolder;
        }

        private TPlaceHolder GetPlaceHolder<TRenderer, TPlaceHolder>(List<LabelRendererBase> rendererList, int index)
            where TRenderer : LabelRendererBase where TPlaceHolder: PlaceHolderBase
        {
            var renderer = rendererList[index] as TRenderer;
            Assert.IsNotNull(renderer);
            var placeHolders = GetPlaceHolder(renderer);
            Assert.AreEqual(1, placeHolders.Count);
            var placeHolder = placeHolders[0] as TPlaceHolder;
            Assert.IsNotNull(placeHolder);

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
                var actualValue = GetPrivateField<object>(placeHolder.GetType(), name, placeHolder);
                AssertObject(expectedValues[name], actualValue);
            }
        }

        #endregion
    }
}