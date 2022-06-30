using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace CosmoService.Code.Producer.PrintReportCommand
{
    public class PrintPdfCommandProducer : CosmoService.Code.Producer.CommandProducerBase<IPrintReport>
    {
        public PrintPdfCommandProducer(string queueName, IMessageManager<IPrintReport> manager) 
            : base(queueName, manager) { }

        protected override async Task SendMessageAsync(IPrintReport message)
        {
            var endPoint = await Bus.GetSendEndpoint(new Uri($"queue:{QueueName}"));
            await endPoint.Send<IPrintPdfReport>(message);
        }

        protected override async Task PostMessageAsync(IPrintReport message)
        {
            await Manager.Post(message);
        }

        protected override async Task DeleteMessageAsync(Guid messageId)
        {
            await Manager.Delete(messageId);
        }
    }
}