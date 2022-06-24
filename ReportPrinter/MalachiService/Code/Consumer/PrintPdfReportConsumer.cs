using System.Threading.Tasks;
using MassTransit;
using RaphaelService;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace MalachiService.Code.Consumer
{
    public  class PrintPdfReportConsumer : IConsumer<IPrintPdfReport>
    {
        public async Task Consume(ConsumeContext<IPrintPdfReport> context)
        {
            var msg = context.Message;
            var a = new Class1();
            await a.DoSometing();
        }
    }
}