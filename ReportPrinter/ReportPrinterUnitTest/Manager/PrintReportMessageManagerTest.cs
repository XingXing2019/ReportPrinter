using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.Manager
{
    public class PrintReportMessageManagerTest : TestBase<IPrintReport>
    {
        public PrintReportMessageManagerTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
        }

        [TearDown]
        public void TearDown()
        {
            Manager.DeleteAll();
        }

        [Test]
        public async Task TestPrintReportMessageManager_Get()
        {
            try
            {
                var expectedMessage = CreateMessage(ReportTypeEnum.PDF.ToString());
                var mgr = new PrintReportMessageManager();

                await mgr.Post(expectedMessage);

                var actualMessage = await mgr.Get(expectedMessage.MessageId);
                Assert.NotNull(actualMessage);
                AssetMessage(expectedMessage, actualMessage);
                Assert.AreEqual(MessageStatus.Publish.ToString(), actualMessage.Status);

                await mgr.PatchStatus(expectedMessage.MessageId, MessageStatus.Receive);
                actualMessage = await mgr.Get(expectedMessage.MessageId);
                Assert.AreEqual(MessageStatus.Receive.ToString(), actualMessage.Status);

                await mgr.Delete(expectedMessage.MessageId);
                actualMessage = await mgr.Get(expectedMessage.MessageId);
                Assert.IsNull(actualMessage);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task TestPrintReportMessageManager_GetAll()
        {
            try
            {
                var mgr = new PrintReportMessageManager();
                var expectedMessages = new List<IPrintReport>();
                
                for (int i = 0; i < 10; i++)
                {
                    var expectedMessage = CreateMessage(ReportTypeEnum.PDF.ToString());
                    expectedMessages.Add(expectedMessage);
                    await mgr.Post(expectedMessage);
                }

                var messages = await mgr.GetAll();
                Assert.AreEqual(10, messages.Count);

                foreach (var message in messages)
                {
                    var expectedMessage = expectedMessages.FirstOrDefault(x => x.MessageId == message.MessageId);
                    AssetMessage(expectedMessage, message);
                }

                await mgr.DeleteAll();
                messages = await mgr.GetAll();
                Assert.AreEqual(0, messages.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}