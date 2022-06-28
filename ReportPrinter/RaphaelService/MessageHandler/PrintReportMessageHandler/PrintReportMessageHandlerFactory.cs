using System;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelService.MessageHandler.PrintReportMessageHandler
{
    public class PrintReportMessageHandlerFactory
    {
        public static IMessageHandler CreatePrintReportMessageHandler(string reportType)
        {
            var procName = $"PrintReportMessageHandlerFactory.{nameof(CreatePrintReportMessageHandler)}";

            if (reportType == ReportTypeEnum.PDF.ToString())
                return new PrintPdfReportMessageHandler();
            else if (reportType == ReportTypeEnum.Label.ToString())
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