using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CosmoService.Code.Producer.PrintReportCommand;
using NUnit.Framework;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
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
    }
}