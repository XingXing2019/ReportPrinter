using System;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace CosmoService.Code.Producer
{
    public class CommandProducerFactory
    {
        public static CommandProducerBase CreateCommandProducer(string queueName)
        {
            var procName = $"CommandProducerFactory.{nameof(CreateCommandProducer)}";

            switch (queueName)
            {
                case QueueName.PDF_QUEUE:
                    return new PrintPdfCommandProducer(queueName);
                case QueueName.LABEL_QUEUE:
                    return new PrintLabelCommandProducer(queueName);
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