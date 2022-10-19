using System;
using NUnit.Framework;
using RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.MessageHandler.PrintReportMessageHandler
{
    public class PrintReportMessageHandlerFactoryTest
    {
        [Test]
        [TestCase(ReportTypeEnum.PDF, typeof(PrintPdfMessageHandler))]
        [TestCase(ReportTypeEnum.Label, typeof(PrintLabelMessageHandler))]
        [TestCase(-1, null)]
        public void TestCreatePrintReportMessageHandler(ReportTypeEnum reportType, Type expectedHandleType)
        {
            try
            {
                var handler = PrintReportMessageHandlerFactory.CreatePrintReportMessageHandler(reportType);
                Assert.AreEqual(expectedHandleType, handler.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid report type: {reportType}";
                var actualError = ex.Message;
                Assert.AreEqual(expectedError, actualError);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}