using System.Threading.Tasks;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace RaphaelService.MessageHandler
{
    public interface IMessageHandler
    {
        Task Handle(IMessage message);
    }
}