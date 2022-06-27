using System.Threading.Tasks;
using ReportPrinterDatabase.Manager.MessageManager;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace CosmoService.Code.Producer
{
    public class PrintLabelCommandProducer : CommandProducerBase
    {
        public PrintLabelCommandProducer(string queueName, IMessageManager manager)
            : base(queueName, manager) { }

        protected override Task SendMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }

        protected override Task PostMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}