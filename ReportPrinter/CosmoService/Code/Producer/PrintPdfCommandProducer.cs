using System;
using System.Threading.Tasks;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace ProctorService.Code.Producer
{
    public class PrintPdfCommandProducer : CommandProducerBase
    {
        public PrintPdfCommandProducer(string queueName) 
            : base(queueName) { }

        protected override async Task SendMessage(object message)
        {
            var endPoint = await Bus.GetSendEndpoint(new Uri($"queue:{QueueName}"));
            await endPoint.Send<IPrintPdfReport>(message);
        }
    }
}