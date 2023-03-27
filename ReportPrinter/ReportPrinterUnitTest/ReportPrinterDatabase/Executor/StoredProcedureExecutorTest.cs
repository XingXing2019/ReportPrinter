using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportSqlVariable;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Executor
{
    public class StoredProcedureExecutorTest : DatabaseTestBase<IPrintReport>
    {
        private readonly StoredProcedureExecutor _executor;
        public StoredProcedureExecutorTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
            _executor = new StoredProcedureExecutor();
        }

        [TearDown]
        public void TearDown()
        {
            Manager.DeleteAll();
        }

        [Test]
        public async Task TestExecuteNonQueryAsync()
        {
            var expectedMessage = CreateMessage(ReportTypeEnum.PDF);

            var sp = new PostPrintReportMessage(
                expectedMessage.MessageId,
                expectedMessage.CorrelationId,
                expectedMessage.ReportType,
                expectedMessage.TemplateId,
                expectedMessage.PrinterId,
                expectedMessage.NumberOfCopy,
                expectedMessage.HasReprintFlag);

            var spList = new List<StoredProcedureBase> { sp };
            spList.AddRange(expectedMessage.SqlVariables.Select(x => new PostPrintReportSqlVariable(expectedMessage.MessageId, x.Name, x.Value)));
            var rows = await _executor.ExecuteNonQueryAsync(spList.ToArray());

            var actualMessage = await Manager.Get(expectedMessage.MessageId);

            try
            {
                Assert.AreEqual(expectedMessage.SqlVariables.Count + 1, rows);
                Assert.IsNotNull(actualMessage);
                AssertHelper.AssetMessage(expectedMessage, actualMessage);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task TestExecuteQueryAsync()
        {
            var expectedMessage = CreateMessage(ReportTypeEnum.PDF);
            await Manager.Post(expectedMessage);

            var messageId = expectedMessage.MessageId;
            var message = await _executor.ExecuteQueryOneAsync<PrintReportMessage>(new GetPrintReportMessage(messageId));
            var sqlVariables = await _executor.ExecuteQueryBatchAsync<PrintReportSqlVariable>(new GetPrintReportSqlVariable(messageId));
            var actualMessage = CreateMessage(message, sqlVariables);

            try
            {
                Assert.IsNotNull(actualMessage);
                AssertHelper.AssetMessage(expectedMessage, actualMessage);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}