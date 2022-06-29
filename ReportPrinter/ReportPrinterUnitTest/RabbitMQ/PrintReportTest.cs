using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoService.Code.Producer.PrintReportCommand;
using NUnit.Framework;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Config.Configuration;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace ReportPrinterUnitTest.RabbitMQ
{
    public class PrintReportTest : TestBase<IPrintReport>
    {
        private const string _serviceName = "MalachiService";

        public PrintReportTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
        }

        [TearDown]
        public void TearDown()
        {
            Manager.DeleteAll();
        }

        [Test]
        [TestCase(QueueName.PDF_QUEUE, ReportTypeEnum.PDF)]
        public async Task TestPrintReport(string queueName, ReportTypeEnum reportType)
        {
            try
            {
                var mgr = new PrintReportMessageManager();
                var producer = PrintReportProducerFactory.CreatePrintReportProducer(queueName, mgr);
                var expectedMessage = CreateMessage(reportType.ToString());

                await producer.ProduceAsync(expectedMessage);

                Thread.Sleep(1000 * 1);
                var actualMessage = await Manager.Get(expectedMessage.MessageId);
                Assert.NotNull(actualMessage);
                AssetMessage(expectedMessage, actualMessage);
                Assert.AreEqual(MessageStatus.Publish.ToString(), actualMessage.Status);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #region Helper

        private IPrintReport CreateMessage(string reportType)
        {
            var messageId = Guid.NewGuid();
            var correlationId = Guid.NewGuid();
            var templateId = "Template1";
            var printerId = "Printer1";
            var numberOfCopy = 3;
            var hasReprintFlag = true;
            var sqlVariables = new List<SqlVariable>
            {
                new SqlVariable { Name = "Name1", Value = "Value1" },
                new SqlVariable { Name = "Name2", Value = "Value2" },
            };
            
            var expectedMessage = PrintReportMessageFactory.CreatePrintReportMessage(reportType);

            expectedMessage.MessageId = messageId;
            expectedMessage.CorrelationId = correlationId;
            expectedMessage.TemplateId = templateId;
            expectedMessage.PrinterId = printerId;
            expectedMessage.NumberOfCopy = numberOfCopy;
            expectedMessage.HasReprintFlag = hasReprintFlag;
            expectedMessage.SqlVariables = sqlVariables;

            return expectedMessage;
        }

        private void AssetMessage(IPrintReport expected, IPrintReport actual)
        {
            Assert.AreEqual(expected.MessageId, actual.MessageId);
            Assert.AreEqual(expected.CorrelationId, actual.CorrelationId);
            Assert.AreEqual(expected.ReportType, actual.ReportType);
            Assert.AreEqual(expected.TemplateId, actual.TemplateId);
            Assert.AreEqual(expected.PrinterId, actual.PrinterId);
            Assert.AreEqual(expected.NumberOfCopy, actual.NumberOfCopy);
            Assert.AreEqual(expected.HasReprintFlag, actual.HasReprintFlag);

            Assert.AreEqual(expected.SqlVariables.Count, actual.SqlVariables.Count);
            foreach (var variable in expected.SqlVariables)
            {
                Assert.IsTrue(actual.SqlVariables.Any(x => x.Name == variable.Name && x.Value == variable.Value));
            }
        }

        #endregion
    }
}