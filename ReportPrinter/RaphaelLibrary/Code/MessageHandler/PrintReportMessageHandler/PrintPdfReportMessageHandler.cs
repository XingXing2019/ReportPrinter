﻿using System.Threading.Tasks;
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

            var manager = new PdfDocumentManager();
            var isSuccess = await Task.Run(() => pdfTemplate.TryCreatePdfReport(manager));

            return isSuccess;
        }
    }
}