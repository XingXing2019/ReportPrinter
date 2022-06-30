using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.Manager
{
    public class PrintReportMessageManagerTest : TestBase<IPrintReport>
    {
        public PrintReportMessageManagerTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
        }

        [Test]
        public async Task TestPrintReportMessageManager()
        {
            try
            {
                var expectedMessage = CreateMessage(ReportTypeEnum.PDF.ToString());
                var mgr = new PrintReportMessageManager();

                await mgr.Post(expectedMessage);

                var actualMessage = await Manager.Get(expectedMessage.MessageId);
                Assert.NotNull(actualMessage);
                AssetMessage(expectedMessage, actualMessage);
                Assert.AreEqual(MessageStatus.Publish.ToString(), actualMessage.Status);

                await mgr.PatchStatus(expectedMessage.MessageId, MessageStatus.Receive);
                actualMessage = await Manager.Get(expectedMessage.MessageId);
                Assert.AreEqual(MessageStatus.Receive.ToString(), actualMessage.Status);

                await mgr.Delete(expectedMessage.MessageId);
                actualMessage = await Manager.Get(expectedMessage.MessageId);
                Assert.IsNull(actualMessage);

                await mgr.DeleteAll();
                var messages = await Manager.GetAll();
                Assert.AreEqual(0, messages.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}