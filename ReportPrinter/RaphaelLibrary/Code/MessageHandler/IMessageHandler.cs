using System.Threading.Tasks;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.MessageHandler
{
    public interface IMessageHandler
    {
        Task<bool> Handle(IPrintReport message);
    }
}