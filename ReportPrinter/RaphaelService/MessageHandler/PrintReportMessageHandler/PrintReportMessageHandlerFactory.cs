using System;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelService.MessageHandler.PrintReportMessageHandler
{
    public class PrintReportMessageHandlerFactory
    {
        public static IMessageHandler CreatePrintReportMessageHandler(ReportTypeEnum reportType)
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