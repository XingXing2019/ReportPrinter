using System.Collections.Generic;
using System.Linq;
using RaphaelLibrary.Code.Render.PDF.Manager;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfReportHeader : PdfStructureBase
    {
        public PdfReportHeader(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfReportHeader, rendererNames) { }

        public override bool TryRenderPdfStructure(PdfDocumentManager manager)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryRenderPdfStructure)}";

            manager.CurrentPage = 0;
            if (PdfRendererList.Any(renderer => !renderer.TryRenderPdf(manager)))
                return false;

            Logger.Info($"Success to render: {Position} for message: {manager.MessageId}", procName);
            return true;
        }
    }
}