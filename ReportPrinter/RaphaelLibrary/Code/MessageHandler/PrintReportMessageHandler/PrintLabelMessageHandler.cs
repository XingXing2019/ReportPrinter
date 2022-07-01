using System.Threading.Tasks;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintLabelMessageHandler : IMessageHandler
    {
        public async Task Handle(IMessage message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Handle)}";
            await Task.Run(() => Logger.Debug($"Process message: {message.MessageId}", procName));
        }
    }
}