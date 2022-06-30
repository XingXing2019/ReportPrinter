using System.Threading.Tasks;
using ReportPrinterLibrary.Code.RabbitMQ.Message;

namespace RaphaelLibrary.Code.MessageHandler
{
    public interface IMessageHandler
    {
        Task Handle(IMessage message);
    }
}