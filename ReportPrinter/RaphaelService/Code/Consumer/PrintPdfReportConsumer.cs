using System;
using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

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
            Logger.LogJson($"{this.GetType().Name} receive message", message, procName);

            await PatchMessageStatus(message, MessageStatus.Receive);

            try
            {
                var isSuccess = await ConsumeMessage(message);
                var status = isSuccess ? MessageStatus.Complete : MessageStatus.Error;
                await PatchMessageStatus(message, status);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during consuming message: {message.MessageId}. Ex: {ex.Message}", procName);
                await PatchMessageStatus(message, MessageStatus.Error);
            }
        }
    }
}