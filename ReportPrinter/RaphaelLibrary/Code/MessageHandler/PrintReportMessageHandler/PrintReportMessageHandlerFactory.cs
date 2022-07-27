using System;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintReportMessageHandlerFactory
    {
        public static PrintReportMessageHandlerBase CreatePrintReportMessageHandler(ReportTypeEnum reportType)
        {
            var procName = $"PrintReportMessageHandlerFactory.{nameof(CreatePrintReportMessageHandler)}";

            if (reportType == ReportTypeEnum.PDF)
                return new PrintPdfReportMessageHandler();
            else if (reportType == ReportTypeEnum.Label)
                return new PrintLabelMessageHandler();
            else
            {
                var error = $"Invalid report type: {reportType}";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}