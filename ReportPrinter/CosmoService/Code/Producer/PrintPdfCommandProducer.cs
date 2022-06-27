using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Context;
using ReportPrinterDatabase.Entity;
using ReportPrinterDatabase.Manager.MessageManager;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace CosmoService.Code.Producer
{
    public class PrintPdfCommandProducer : CommandProducerBase
    {
        public PrintPdfCommandProducer(string queueName, IMessageManager manager) 
            : base(queueName, manager) { }

        protected override async Task SendMessage(IMessage message)
        {
            var endPoint = await Bus.GetSendEndpoint(new Uri($"queue:{QueueName}"));
            await endPoint.Send<IPrintPdfReport>(message);
        }

        protected override async Task PostMessage(IMessage message)
        {
            await Manager.Post(message);
        }
    }
}