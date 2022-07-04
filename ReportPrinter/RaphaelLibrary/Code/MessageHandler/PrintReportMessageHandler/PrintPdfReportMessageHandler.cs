using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.PDF;
using RaphaelLibrary.Code.Render.PDF.Manager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintPdfReportMessageHandler : IMessageHandler
    {
        public async Task<bool> Handle(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Handle)}";
            
            var pdfTemplateId = message.TemplateId;

            if (!PdfTemplateManager.Instance.TryGetPdfTemplate(pdfTemplateId, out var pdfTemplate))
            {
                return false;
            }

            var sqlVariables = message.SqlVariables.ToDictionary(x => x.Name, x => new SqlVariable { Name = x.Name, Value = x.Value });
            SqlVariableManager.Instance.StoreSqlVariables(message.MessageId, sqlVariables);

            var isSuccess = await Task.Run(() => pdfTemplate.TryCreatePdfReport(message.MessageId));

            return isSuccess;
        }
    }
}