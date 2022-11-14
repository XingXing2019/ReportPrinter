using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Renderer
{
    public class LabelSqlRendererTest : TestBase
    {
        private const string S_FILE_PATH = @".\RaphaelLibrary\Render\Label\Renderer\TestFile\SqlTemplate.xml";
        private const string S_LINE = "%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"TestSqlPlaceHolder\"/>%%%";

        [Test]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"TestSqlPlaceHolder\"/>%%%", true)]
        [TestCase("%%%<Sql SqlResColumn=\"\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"TestSqlPlaceHolder\"/>%%%", false)]
        [TestCase("%%%<Sql SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"TestSqlPlaceHolder\"/>%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"\" SqlId=\"TestSqlPlaceHolder\"/>%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlId=\"TestSqlPlaceHolder\"/>%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"\"/>%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" />%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"WrongSqlTemplateId\" SqlId=\"TestSqlPlaceHolder\"/>%%%", false)]
        [TestCase("%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"WrongSqlId\"/>%%%", false)]
        public async Task TestReadLine(string line, bool expectedRes)
        {
            var message = CreateMessage(ReportTypeEnum.PDF);

            var sqlTemplate = await SetupSqlTest(S_FILE_PATH, message, expectedRes);
            var isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);

            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
            sqlTemplateList.Add("TestSqlPlaceHolder", sqlTemplate);


            try
            {
                var renderer = new LabelSqlRenderer(0);
                var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                var actualRes = renderer.ReadLine(line, deserializer, LabelElementHelper.S_SQL);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var placeHolders = GetPrivateField<List<PlaceHolderBase>>(renderer, "PlaceHolders");

                    Assert.AreEqual(1, placeHolders.Count);
                    var placeHolder = placeHolders[0] as SqlPlaceHolder;
                    Assert.IsNotNull(placeHolder);

                    var actualSql = GetPrivateField<Sql>(placeHolder, "_sql");
                    var actualSqlResColumn = GetPrivateField<SqlResColumn>(placeHolder, "_sqlResColumn");

                    AssertObject(sql, actualSql);
                    AssertObject(new SqlResColumn("PRM_CorrelationId"), actualSqlResColumn);
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

            var sqlTemplate = await SetupSqlTest(S_FILE_PATH, message, true);
            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
            sqlTemplateList.Add("TestSqlPlaceHolder", sqlTemplate);
            
            try
            {
                var renderer = new LabelSqlRenderer(0);
                var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                var isSuccess = renderer.ReadLine(S_LINE, deserializer, LabelElementHelper.S_SQL);
                Assert.IsTrue(isSuccess);

                var cloned = renderer.Clone();
                AssertObject(renderer, cloned);
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
        [TestCase(true)]
        [TestCase(false)]
        public async Task TestTryRenderLabel(bool expectedRes)
        {
            var message = CreateMessage(ReportTypeEnum.PDF);

            var sqlTemplate = await SetupSqlTest(S_FILE_PATH, message, expectedRes);
            var sqlTemplateList = GetPrivateField<Dictionary<string, SqlElementBase>>(SqlTemplateManager.Instance, "_sqlTemplateList");
            sqlTemplateList.Add("TestSqlPlaceHolder", sqlTemplate);
            
            try
            {
                var renderer = new LabelSqlRenderer(0);
                var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                var actualRes = renderer.ReadLine(S_LINE, deserializer, LabelElementHelper.S_SQL);
                Assert.IsTrue(actualRes);

                var manager = new LabelManager(new[] { S_LINE }, message.MessageId);
                actualRes = renderer.TryRenderLabel(manager);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    Assert.AreEqual(message.CorrelationId.ToString(), manager.Lines[0]);
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
    }
}