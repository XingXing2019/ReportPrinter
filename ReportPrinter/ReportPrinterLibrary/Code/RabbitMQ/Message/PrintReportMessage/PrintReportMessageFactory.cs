using System;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage
{
    public class PrintReportMessageFactory
    {
        public static IPrintReport CreatePrintReportMessage(string reportType)
        {
            var procName = $"PrintReportMessageFactory.{nameof(CreatePrintReportMessage)}";

            if (reportType == ReportTypeEnum.PDF.ToString()) 
                return new PrintPdfReport();
            else if (reportType == ReportTypeEnum.Label.ToString()) 
                return new PrintLabelReport();
            else
            {
                var error = $"Invalid report type: {reportType}";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}