using System.Threading.Tasks;
using MassTransit;
using RaphaelService;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace MalachiService.Code.Consumer
{
    public class PrintPdfReportConsumer : PrintReportConsumerBase, IConsumer<IPrintPdfReport>
    {
        public PrintPdfReportConsumer(IPrintReportMessageManager<IPrintReport> manager) 
            : base(manager) { }

        public async Task Consume(ConsumeContext<IPrintPdfReport> context)
        {
            var procName = $"{this.GetType().Name}.{nameof(Consume)}";

            var message = context.Message;
            Logger.LogJson($"{nameof(PrintLabelReportConsumer)} receive message", message, procName);

            await PatchMessageStatus(message);

            var a = new Class1();
        }
    }
}