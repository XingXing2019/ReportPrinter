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
        [TestCase(typeof(PrintReportMessageSPManager))]
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
        [TestCase(typeof(PrintReportMessageSPManager))]
        [TestCase(typeof(PrintReportMessageEFCoreManager))]
        public async Task TestPrintReportMessageManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (IPrintReportMessageManager<IPrintReport>)Activator.CreateInstance(managerType);
                var expectedMessages = new List<IPrintReport>();
                var messagesToDelete = new List<Guid>();

                for (int i = 0; i < 10; i++)
                {
                    var expectedMessage = CreateMessage(ReportTypeEnum.PDF);
                    expectedMessages.Add(expectedMessage);
                    await mgr.Post(expectedMessage);

                    if (i < 5)
                    {
                        messagesToDelete.Add(expectedMessage.MessageId);
                    }
                }

                var actualMessages = await mgr.GetAll();
                Assert.AreEqual(10, actualMessages.Count);

                foreach (var expectedMessage in expectedMessages)
                {
                    var actualMessage = actualMessages.Single(x => x.MessageId == expectedMessage.MessageId);
                    AssertHelper.AssetMessage(expectedMessage, actualMessage);
                }

                await mgr.Delete(messagesToDelete);
                actualMessages = await mgr.GetAll();

                Assert.AreEqual(5, actualMessages.Count);
                expectedMessages = expectedMessages.Where(x => !messagesToDelete.Contains(x.MessageId)).ToList();

                foreach (var expectedMessage in expectedMessages)
                {
                    var actualMessage = actualMessages.Single(x => x.MessageId == expectedMessage.MessageId);
                    AssertHelper.AssetMessage(expectedMessage, actualMessage);
                }

                await mgr.DeleteAll();
                actualMessages = await mgr.GetAll();
                Assert.AreEqual(0, actualMessages.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}