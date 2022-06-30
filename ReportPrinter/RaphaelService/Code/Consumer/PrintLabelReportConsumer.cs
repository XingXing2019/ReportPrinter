using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelService.Code.Consumer
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