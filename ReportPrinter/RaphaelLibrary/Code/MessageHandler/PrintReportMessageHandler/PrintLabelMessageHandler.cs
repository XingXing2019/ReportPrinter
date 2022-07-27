using RaphaelLibrary.Code.Init;
using RaphaelLibrary.Code.Init.Label;

namespace RaphaelLibrary.Code.MessageHandler.PrintReportMessageHandler
{
    public class PrintLabelMessageHandler : PrintReportMessageHandlerBase
    {
        protected override bool TryGetReportTemplate(string templateId, out ITemplate template)
        {
            return LabelTemplateManager.Instances.TryGetReportTemplate(templateId, out template);
        }
    }
}