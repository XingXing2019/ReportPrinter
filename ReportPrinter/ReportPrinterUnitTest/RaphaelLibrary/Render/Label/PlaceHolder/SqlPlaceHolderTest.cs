using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class SqlPlaceHolderTest : PlaceHolderTestBase
    {
        private const string S_PLACE_HOLDER = "%%%<Sql SqlResColumn=\"PRM_CorrelationId\" SqlTemplateId=\"TestSqlPlaceHolder\" SqlId=\"TestSqlPlaceHolder\" />%%%";

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task TestTryGetPlaceHolderValue(bool expectedRes)
        {
            var message = CreateMessage(ReportTypeEnum.PDF);
            var databaseManager = new PrintReportMessageEFCoreManager();

            if (expectedRes)
            {
                await databaseManager.Post(message);
            }

            var manager = CreateLabelManager(S_PLACE_HOLDER, message.MessageId);

            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\SqlTemplate.xml";
            var node = GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();
            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);
            
            var cacheManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            var cacheManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(cacheManagerType);

            var sqlVariables = new Dictionary<string, SqlVariable>
            {
                { "MessageId", new SqlVariable { Name = "MessageId", Value = message.MessageId.ToString() } }
            };
            cacheManager.StoreSqlVariables(message.MessageId, sqlVariables);

            try
            {
                var sqlResColumn = new SqlResColumn("PRM_CorrelationId");
                var sqlPlaceHolder = new SqlPlaceHolder(S_PLACE_HOLDER, sql, sqlResColumn);
                var actualRes = sqlPlaceHolder.TryReplacePlaceHolder(manager, 0);

                Assert.AreEqual(expectedRes, actualRes);
                if (expectedRes)
                {
                    var expectedValue = message.CorrelationId.ToString();
                    Assert.AreEqual(expectedValue, manager.Lines[0]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                await databaseManager.DeleteAll();
            }
        }
    }
}