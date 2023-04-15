using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Print;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Print
{
    public class PrinterFactoryTest
    {
        [Test]
        [TestCase(ReportTypeEnum.PDF, typeof(PdfPrinter))]
        [TestCase(ReportTypeEnum.Label, typeof(LabelPrinter))]
        [TestCase(2, null)]
        public void TestCreatePrinter(ReportTypeEnum reportType, Type expectedType)
        {
            try
            {
                var printer = PrinterFactory.CreatePrinter(reportType);
                Assert.AreEqual(expectedType, printer.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid report type: {reportType} for creating printer";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}