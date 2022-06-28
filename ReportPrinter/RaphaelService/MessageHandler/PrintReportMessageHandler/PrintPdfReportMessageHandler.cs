using System.Threading.Tasks;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace RaphaelService.MessageHandler.PrintReportMessageHandler
{
    public class PrintPdfReportMessageHandler : IMessageHandler
    {
        public async Task Handle(IMessage message)
        {
            //throw new System.NotImplementedException();
        }
    }
}