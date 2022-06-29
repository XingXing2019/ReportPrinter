using System.Threading;
using System.Threading.Tasks;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace RaphaelService.MessageHandler.PrintReportMessageHandler
{
    public class PrintPdfReportMessageHandler : IMessageHandler
    {
        public async Task Handle(IMessage message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Handle)}";
            //Thread.Sleep(5000);
            await Task.Run(() => Logger.Debug($"Process message: {message.MessageId}", procName));
        }
    }
}