using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using System.Collections.Generic;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.Model;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelValidationRendererTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\SqlTemplate.xml";
        private const string S_LINE = "%%%<Validation\r\n\tType=\"Text\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                                      "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueContent\"\r\n\tFalseStructure=\"FalseContent\"\r\n/>%%%";

        [Test]
        [TestCaseSource(nameof(TestReadLineTestCases))]
        public async Task TestReadLine(string line, bool expectedRes, SqlResColumn sqlResColumn, ValidationModel validationModel)
        {
            var message = CreateMessage(ReportTypeEnum.PDF);

            var sqlTemplate = await SetupSqlTest(S_FILE_PATH, message, expectedRes);
            var isSuccess = sqlTemplate.TryGetSql("TestLabelRenderer", out var sql);
            Assert.IsTrue(isSuccess);
            
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructure(deserializer);

            try
            {
                var renderer = new LabelValidationRenderer(0);
                var actualRes = renderer.ReadLine(line, deserializer, LabelElementHelper.S_VALIDATION);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var placeHolders = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");

                    Assert.AreEqual(1, placeHolders.Count);
                    var placeHolder = placeHolders[0] as ValidationPlaceHolder;
                    Assert.IsNotNull(placeHolder);

                    var actualValidationType = GetPrivateField<ValidationType>(placeHolder, "_validationType");
                    Assert.AreEqual(validationModel.ValidationType, actualValidationType);

                    var actualExpectedValue = GetPrivateField<string>(placeHolder, "_expectedValue");
                    Assert.AreEqual(validationModel.ExpectedValue, actualExpectedValue);

                    var actualComparator = GetPrivateField<Comparator>(placeHolder, "_comparator");
                    Assert.AreEqual(validationModel.Comparator, actualComparator);

                    var actualSql = GetPrivateField<Sql>(placeHolder, "_sql");
                    AssertHelper.AssertObject(sql, actualSql);

                    var actualSqlResColumn = GetPrivateField<SqlResColumn>(placeHolder, "_sqlResColumn");
                    AssertHelper.AssertObject(sqlResColumn, actualSqlResColumn);

                    if (validationModel.ValidationType == ValidationType.Text)
                    {
                        var actualTrueContent = GetPrivateField<string>(placeHolder, "_trueContent");
                        Assert.AreEqual(validationModel.TrueContent, actualTrueContent);

                        var actualFalseContent = GetPrivateField<string>(placeHolder, "_falseContent");
                        Assert.AreEqual(validationModel.FalseContent, actualFalseContent);
                    }
                    else if (validationModel.ValidationType == ValidationType.Structure)
                    {
                        var actualTrueStructure = GetPrivateField<IStructure>(placeHolder, "_trueStructure");
                        AssertHelper.AssertObject(validationModel.TrueStructure, actualTrueStructure);

                        var actualFalseStructure = GetPrivateField<IStructure>(placeHolder, "_falseStructure");
                        AssertHelper.AssertObject(validationModel.FalseStructure, actualFalseStructure);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                await new PrintReportMessageEFCoreManager().DeleteAll();
                SqlVariableMemoryCacheManager.Instance.Reset();
            }
        }

        [Test]
        public async Task TestClone()
        {
            var message = CreateMessage(ReportTypeEnum.PDF);
            await SetupSqlTest(S_FILE_PATH, message, true);

            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructure(deserializer);

            try
            {
                var renderer = new LabelValidationRenderer(0);
                var isSuccess = renderer.ReadLine(S_LINE, deserializer, LabelElementHelper.S_VALIDATION);
                Assert.IsTrue(isSuccess);

                var cloned = renderer.Clone();
                AssertHelper.AssertObject(renderer, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                await new PrintReportMessageEFCoreManager().DeleteAll();
                SqlVariableMemoryCacheManager.Instance.Reset();
            }
        }

        [Test]
        public async Task TestTryRenderLabel()
        {
            var message = CreateMessage(ReportTypeEnum.PDF);

            var sqlTemplate = await SetupSqlTest(S_FILE_PATH, message, true);
            var isSuccess = sqlTemplate.TryGetSql("TestLabelRenderer", out var sql);
            Assert.IsTrue(isSuccess);

            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
            SetupLabelStructure(deserializer);

            try
            {
                var renderer = new LabelValidationRenderer(0);
                isSuccess = renderer.ReadLine(S_LINE, deserializer, LabelElementHelper.S_VALIDATION);
                Assert.IsTrue(isSuccess);

                var manager = new LabelManager(new[] { S_LINE }, message.MessageId);
                isSuccess = renderer.TryRenderLabel(manager);

                var expected = "TrueContent";
                Assert.IsTrue(isSuccess);
                Assert.AreEqual(expected, manager.Lines[0]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                await new PrintReportMessageEFCoreManager().DeleteAll();
                SqlVariableMemoryCacheManager.Instance.Reset();
            }
        }


        #region Helper

        private static object[] TestReadLineTestCases()
        {
            var trueContent = "TrueContent";
            var falseContent = "FalseContent";

            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            var trueStructurePath = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\TrueLabelStructure.txt";
            var trueStructure = new LabelStructure("TrueStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            trueStructure.ReadFile(trueStructurePath);

            var falseStructurePath = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\FalseLabelStructure.txt";
            var falseStructure = new LabelStructure("FalseStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            falseStructure.ReadFile(falseStructurePath);

            return new object[]
            {
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Text\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueContent\"\r\n\tFalseStructure=\"FalseContent\"\r\n/>%%%", 
                    true, new SqlResColumn("PRM_Status"), new ValidationModel(ValidationType.Text, Comparator.Equals, "Publish", trueContent, falseContent)
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    true, new SqlResColumn("PRM_Status"), new ValidationModel(ValidationType.Structure, Comparator.Equals, "Publish", trueStructure, falseStructure)
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\t" +
                    "SqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"WrongSqlTemplateId\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"WrongSqlId" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\t/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"WrongTrueStructureId\"\r\n\tFalseStructure=\"FalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
                new object[]
                {
                    "%%%<Validation\r\n\tType=\"Structure\"\r\n\tComparator=\"Equals\"\r\n\tExpectedValue=\"Publish\"\r\n\tSqlTemplateId=\"TestLabelRenderer\"\r\n\tSqlId=\"TestLabelRenderer" +
                    "\"\r\n\tSqlResColumn=\"PRM_Status\"\r\n\tTrueStructure=\"TrueStructureId\"\r\n\tFalseStructure=\"WrongFalseStructureId\"\r\n/>%%%",
                    false, null, null
                },
            };
        }

        private void SetupLabelStructure(LabelDeserializeHelper deserializer)
        {
            var trueStructurePath = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\TrueLabelStructure.txt";
            var trueStructure = new LabelStructure("TrueStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            trueStructure.ReadFile(trueStructurePath);

            var falseStructurePath = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\FalseLabelStructure.txt";
            var falseStructure = new LabelStructure("FalseStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            falseStructure.ReadFile(falseStructurePath);

            var labelStructureList = GetPrivateField<Dictionary<string, IStructure>>(LabelStructureManager.Instance, "_labelStructureList");
            labelStructureList.Add(trueStructure.Id, trueStructure);
            labelStructureList.Add(falseStructure.Id, falseStructure);
        }

        #endregion
    }
}