using System.Threading.Tasks;
using MassTransit;
using RaphaelService;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace MalachiService.Code.Consumer
{
    public  class PrintPdfReportConsumer : IConsumer<IPrintPdfReport>
    {
        public async Task Consume(ConsumeContext<IPrintPdfReport> context)
        {
            var procName = $"{this.GetType().Name}.{nameof(Consume)}";
            var msg = context.Message;
            Logger.LogJson($"{nameof(PrintLabelReportConsumer)} receive message", msg, procName);

            var a = new Class1();
        }
    }
}