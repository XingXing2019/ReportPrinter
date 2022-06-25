using System.Threading.Tasks;

namespace CosmoService.Code.Producer
{
    public class PrintLabelCommandProducer : CommandProducerBase
    {
        public PrintLabelCommandProducer(string queueName)
            : base(queueName) { }

        protected override Task SendMessage(object message)
        {
            throw new System.NotImplementedException();
        }
    }
}