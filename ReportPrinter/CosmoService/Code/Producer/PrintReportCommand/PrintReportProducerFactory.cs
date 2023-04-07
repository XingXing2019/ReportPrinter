using System;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.MessageQueue;

namespace CosmoService.Code.Producer.PrintReportCommand
{
    public class PrintReportProducerFactory
    {
        public static CosmoService.Code.Producer.CommandProducerBase<IPrintReport> CreatePrintReportProducer(string queueName, IManager<IPrintReport> manager)
        {
            var procName = $"CommandProducerFactory.{nameof(CreatePrintReportProducer)}";

            if (queueName == QueueName.PDF_QUEUE) 
                return new PrintPdfCommandProducer(queueName, manager);
            else if (queueName == QueueName.LABEL_QUEUE) 
                return new PrintLabelCommandProducer(queueName, manager);
            else
            {
                var error = $"{queueName} does not have corresponding command producer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}