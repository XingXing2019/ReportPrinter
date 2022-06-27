using System;
using ReportPrinterDatabase.Manager.MessageManager;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace CosmoService.Code.Producer
{
    public class CommandProducerFactory
    {
        public static CommandProducerBase CreateCommandProducer(string queueName, IMessageManager manager)
        {
            var procName = $"CommandProducerFactory.{nameof(CreateCommandProducer)}";

            switch (queueName)
            {
                case QueueName.PDF_QUEUE:
                    return new PrintPdfCommandProducer(queueName, manager);
                case QueueName.LABEL_QUEUE:
                    return new PrintLabelCommandProducer(queueName, manager);
                default:
                {
                    var error = $"{queueName} does not have corresponding command producer";
                    Logger.Error(error, procName);
                    throw new InvalidOperationException(error);
                }
            }
        }
    }
}