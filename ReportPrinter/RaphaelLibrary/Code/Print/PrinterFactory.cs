using System;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Print
{
    public class PrinterFactory
    {
        public static PrinterBase CreatePrinter(ReportTypeEnum reportType)
        {
            var procName = $"PrinterFactory.{nameof(CreatePrinter)}";

            if (reportType == ReportTypeEnum.PDF)
                return new PdfPrinter();
            else if (reportType == ReportTypeEnum.Label)
                return new LabelPrinter();
            else
            {
                var error = $"Invalid report type: {reportType} for creating printer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}