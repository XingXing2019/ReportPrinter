using System.Threading.Tasks;
using MassTransit;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace MalachiService.Code.Consumer
{
    public class PrintLabelReportConsumer : IConsumer<IPrintLabelReport>
    {
        public Task Consume(ConsumeContext<IPrintLabelReport> context)
        {
            throw new System.NotImplementedException();
        }
    }
}