using System;
using System.Threading.Tasks;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace RaphaelService.MessageHandler.PrintReportMessageHandler
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