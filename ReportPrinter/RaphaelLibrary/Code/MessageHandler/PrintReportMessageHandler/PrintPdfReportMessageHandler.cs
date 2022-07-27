using RaphaelLibrary.Code.Init;
using RaphaelLibrary.Code.Init.PDF;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintPdfReportMessageHandler : PrintReportMessageHandlerBase
    {
        protected override bool TryGetReportTemplate(string templateId, out ITemplate template)
        {
            return PdfTemplateManager.Instance.TryGetReportTemplate(templateId, out template);
        }
    }
}