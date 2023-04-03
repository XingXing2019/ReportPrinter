using System;
using System.Threading;
using System.Threading.Tasks;
using CosmoService.Code.Producer.PrintReportCommand;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.MessageQueue;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.RabbitMQ
{
    public class PublishMessageTest : RabbitMQTestBase<IPrintReport>
    {
        public PublishMessageTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
        }

        [TearDown]
        public void TearDown()
        {
            Manager.DeleteAll();
        }

        [Test]
        [TestCase(QueueName.PDF_QUEUE, ReportTypeEnum.PDF, typeof(PrintPdfReport))]
        [TestCase(QueueName.LABEL_QUEUE, ReportTypeEnum.Label, typeof(PrintLabelReport))]
        public async Task TestPublishPrintReport(string queueName, ReportTypeEnum reportType, Type messageType)
        {
            try
            {
                var mgr = new PrintReportMessageSPManager();
                var producer = PrintReportProducerFactory.CreatePrintReportProducer(queueName, mgr);
                var expectedMessage = CreateMessage(reportType);

                await producer.ProduceAsync(expectedMessage);

                Thread.Sleep(1000 * 1);
                var actualMessage = await Manager.Get(expectedMessage.MessageId);
                Assert.NotNull(actualMessage);
                AssertHelper.AssetMessage(expectedMessage, actualMessage);
                Assert.AreEqual(MessageStatus.Publish.ToString(), actualMessage.Status);

                var messages = GetMessages(queueName, messageType);
                Assert.AreEqual(1, messages.Count);

                AssertHelper.AssetMessage(expectedMessage, (IPrintReport)messages[0]);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}