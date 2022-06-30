using System;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace CosmoService.Code.Producer.PrintReportCommand
{
    public class PrintReportProducerFactory
    {
        public static CosmoService.Code.Producer.CommandProducerBase<IPrintReport> CreatePrintReportProducer(string queueName, IMessageManager<IPrintReport> manager)
        {
            var procName = $"CommandProducerFactory.{nameof(CreatePrintReportProducer)}";

            if (queueName == QueueName.PDF_QUEUE) 
                return new PrintPdfCommandProducer(queueName, manager);
            else if (queueName == QueueName.LABEL_QUEUE) 
                return new CosmoService.Code.Producer.PrintReportCommand.PrintLabelCommandProducer(queueName, manager);
            else
            {
                var error = $"{queueName} does not have corresponding command producer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}