using System;
using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelService.Code.Consumer
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

            await PatchMessageStatus(message, MessageStatus.Receive);

            try
            {
                await ConsumeMessage(message);
                await PatchMessageStatus(message, MessageStatus.Complete);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during consuming message: {message.MessageId}. Ex: {ex.Message}", procName);
                await PatchMessageStatus(message, MessageStatus.Error);
            }
        }
    }
}