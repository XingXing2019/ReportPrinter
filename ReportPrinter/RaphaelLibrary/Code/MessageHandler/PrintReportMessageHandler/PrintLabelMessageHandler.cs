using RaphaelLibrary.Code.Init;
using RaphaelLibrary.Code.Init.Label;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintLabelMessageHandler : PrintReportMessageHandlerBase
    {
        protected override bool TryGetReportTemplate(string templateId, out TemplateBase template)
        {
            return LabelTemplateManager.Instances.TryGetReportTemplate(templateId, out template);
        }
    }
}