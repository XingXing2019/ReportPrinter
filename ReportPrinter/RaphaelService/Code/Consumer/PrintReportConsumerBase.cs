using System.Threading.Tasks;
using RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelService.Code.Consumer
{
    public abstract class PrintReportConsumerBase
    {
        protected readonly IPrintReportMessageManager<IPrintReport> Manager;

        protected PrintReportConsumerBase(IPrintReportMessageManager<IPrintReport> manager)
        {
            Manager = manager;
        }

        protected async Task PatchMessageStatus(IPrintReport message, MessageStatus status)
        {
            await Manager.PatchStatus(message.MessageId, status);
        }

        protected async Task<bool> ConsumeMessage(IPrintReport message)
        {
            var handler = PrintReportMessageHandlerFactory.CreatePrintReportMessageHandler(message.ReportType);
            return await handler.Handle(message);
        }
    }
}