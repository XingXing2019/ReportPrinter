using System.Collections.Generic;
using System.Linq;
using RaphaelLibrary.Code.Render.PDF.Manager;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfReportFooter : PdfStructureBase
    {
        public PdfReportFooter(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfReportFooter, rendererNames) { }

        public override bool TryRenderPdfStructure(PdfDocumentManager manager)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryRenderPdfStructure)}";

            manager.CurrentPage = manager.Pdf.PageCount - 1;
            if (PdfRendererList.Any(x => !x.TryRenderPdf(manager)))
                return false;

            Logger.Info($"Success to render: {Position} for message: {manager.MessageId}", procName);
            return true;
        }
    }
}