using System;
using NUnit.Framework;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RabbitMQ
{
    public class PrintReportMessageFactoryTest
    {
        [Test]
        [TestCase("PDF", typeof(PrintPdfReport))]
        [TestCase("Label", typeof(PrintLabelReport))]
        [TestCase("InvalidType", null)]
        public void TestCreatePrintReportMessage(string reportType, Type expectedType)
        {
            try
            {
                var message = PrintReportMessageFactory.CreatePrintReportMessage(reportType);
                Assert.AreEqual(expectedType, message.GetType());
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