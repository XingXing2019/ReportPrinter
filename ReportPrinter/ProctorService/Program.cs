using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using ProctorService.Code.Producer;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;

namespace ProctorService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var producer = CommandProducerFactory.CreateCommandProducer(QueueName.PDF_QUEUE);
            object message = new
            {
                MessageId = InVar.Id,
                TemplateId = "template",
                PrinterId = "printer",
                SqlVariables = new[]
                {
                    new { Name = "a", Value = "1" },
                    new { Name = "b", Value = "2" },
                },
                NumberOfCopy = 1,
                HasReprintFlag = false
            };

            await producer.Produce(message);
        }
    }
}
