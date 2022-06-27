using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace MalachiService.Code.Consumer
{
    public class PrintLabelReportConsumer : PrintReportConsumerBase, IConsumer<IPrintLabelReport>
    {
        public PrintLabelReportConsumer(IPrintReportMessageManager<IPrintReport> manager) 
            : base(manager) { }

        public Task Consume(ConsumeContext<IPrintLabelReport> context)
        {
            throw new System.NotImplementedException();
        }
    }
}