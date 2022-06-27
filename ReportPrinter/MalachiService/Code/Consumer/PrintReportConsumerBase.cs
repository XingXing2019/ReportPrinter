using System.Threading.Tasks;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace MalachiService.Code.Consumer
{
    public abstract class PrintReportConsumerBase
    {
        protected readonly IPrintReportMessageManager<IPrintReport> Manager;

        protected PrintReportConsumerBase(IPrintReportMessageManager<IPrintReport> manager)
        {
            Manager = manager;
        }

        protected async Task PatchMessageStatus(IPrintReport message)
        {
            await Manager.PatchStatus(message.MessageId, MessageStatus.Receive);
        }
    }
}