using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace RabbitMQLibrary.Code.Producer.PrintReportCommand
{
    public class PrintLabelCommandProducer : CommandProducerBase<IPrintReport>
    {
        public PrintLabelCommandProducer(string queueName, IMessageManager<IPrintReport> manager)
            : base(queueName, manager) { }

        protected override Task SendMessageAsync(IPrintReport message)
        {
            throw new System.NotImplementedException();
        }

        protected override Task PostMessageAsync(IPrintReport message)
        {
            throw new System.NotImplementedException();
        }

        protected override Task DeleteMessageAsync(Guid messageId)
        {
            throw new NotImplementedException();
        }
    }
}