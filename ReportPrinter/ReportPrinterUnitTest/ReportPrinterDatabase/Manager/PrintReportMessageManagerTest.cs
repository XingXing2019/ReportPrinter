using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PrintReportMessageManagerTest : DatabaseTestBase<IPrintReport>
    {
        public PrintReportMessageManagerTest()
        {
            Manager = new PrintReportMessageEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll();
        }

        [Test]
        [TestCase(typeof(PrintReportMessageManager))]
        [TestCase(typeof(PrintReportMessageEFCoreManager))]
        public async Task TestPrintReportMessageManager_Get(Type managerType)
        {
            try
            {
                var expectedMessage = CreateMessage(ReportTypeEnum.PDF);
                var mgr = (IPrintReportMessageManager<IPrintReport>)Activator.CreateInstance(managerType);

                await mgr.Post(expectedMessage);

                var actualMessage = await mgr.Get(expectedMessage.MessageId);
                Assert.NotNull(actualMessage);
                AssertHelper.AssetMessage(expectedMessage, actualMessage);
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
        [TestCase(typeof(PrintReportMessageManager))]
        [TestCase(typeof(PrintReportMessageEFCoreManager))]
        public async Task TestPrintReportMessageManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (IPrintReportMessageManager<IPrintReport>)Activator.CreateInstance(managerType);
                var expectedMessages = new List<IPrintReport>();

                for (int i = 0; i < 10; i++)
                {
                    var expectedMessage = CreateMessage(ReportTypeEnum.PDF);
                    expectedMessages.Add(expectedMessage);
                    await mgr.Post(expectedMessage);
                }

                var messages = await mgr.GetAll();
                Assert.AreEqual(10, messages.Count);

                foreach (var message in messages)
                {
                    var expectedMessage = expectedMessages.FirstOrDefault(x => x.MessageId == message.MessageId);
                    AssertHelper.AssetMessage(expectedMessage, message);
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