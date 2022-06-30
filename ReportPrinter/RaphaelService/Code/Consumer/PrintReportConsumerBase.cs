using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

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

        protected async Task ConsumeMessage(IPrintReport message)
        {
            //var handler = PrintReportMessageHandlerFactory.CreatePrintReportMessageHandler(message.ReportType);
            //await handler.Handle(message);
        }
    }
}