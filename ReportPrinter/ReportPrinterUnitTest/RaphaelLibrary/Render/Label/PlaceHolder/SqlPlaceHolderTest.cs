using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class SqlPlaceHolderTest : PlaceHolderTestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task TestTryReplacePlaceHolder(bool expectedRes)
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\SqlTemplate.xml";
            var message = CreateMessage(ReportTypeEnum.PDF);
            var manager = CreateLabelManager(S_PLACE_HOLDER, message.MessageId);

            var sqlTemplate = await SetupSqlTest(filePath, message, expectedRes);
            var isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);

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
                await new PrintReportMessageEFCoreManager().DeleteAll();
                SqlVariableMemoryCacheManager.Instance.Reset();
            }
        }

        [Test]
        public void TestClone()
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\SqlTemplate.xml";
            var node = TestFileHelper.GetXmlNode(filePath);
            var sqlTemplate = new SqlTemplate();
            var isSuccess = sqlTemplate.ReadXml(node);
            Assert.IsTrue(isSuccess);

            isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);
            var sqlResColumn = new SqlResColumn("PRM_CorrelationId");
            var sqlPlaceHolder = new SqlPlaceHolder(S_PLACE_HOLDER, sql, sqlResColumn);

            try
            {
                var cloned = sqlPlaceHolder.Clone();
                AssertHelper.AssertObject(sqlPlaceHolder, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}