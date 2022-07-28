using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace CosmoService.Code.Producer.PrintReportCommand
{
    public class PrintLabelCommandProducer : CommandProducerBase<IPrintReport>
    {
        public PrintLabelCommandProducer(string queueName, IMessageManager<IPrintReport> manager)
            : base(queueName, manager) { }

        protected override async Task SendMessageAsync(IPrintReport message)
        {
            var endpoint = await Bus.GetSendEndpoint(new Uri($"queue:{QueueName}"));
            await endpoint.Send<IPrintLabelReport>(message);
        }
    }
}