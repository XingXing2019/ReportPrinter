using System.Threading.Tasks;
using RaphaelLibrary.Code.Init;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public abstract class PrintReportMessageHandlerBase : IMessageHandler
    {
        public async Task<bool> Handle(IPrintReport message)
        {
            var templateId = message.TemplateId;

            if (!TryGetReportTemplate(templateId, out var template))
            {
                return false;
            }

            var isSuccess = await Task.Run(() => template.TryCreateReport(message));
            return isSuccess;
        }

        protected abstract bool TryGetReportTemplate(string templateId, out TemplateBase template);
    }
}