using RaphaelLibrary.Code.Init;
using RaphaelLibrary.Code.Init.PDF;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintPdfMessageHandler : PrintReportMessageHandlerBase
    {
        protected override bool TryGetReportTemplate(string templateId, out TemplateBase template)
        {
            return PdfTemplateManager.Instance.TryGetReportTemplate(templateId, out template);
        }
    }
}