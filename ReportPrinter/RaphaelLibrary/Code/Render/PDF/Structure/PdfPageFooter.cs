using System.Collections.Generic;
using System.Linq;
using RaphaelLibrary.Code.Render.PDF.Manager;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfPageFooter : PdfStructureBase
    {
        public PdfPageFooter(HashSet<string> rendererNames)
            : base(PdfStructure.PdfPageFooter, rendererNames) { }

        public override bool TryRenderPdfStructure(PdfDocumentManager manager)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryRenderPdfStructure)}";

            int startPage = 0, endPage = manager.Pdf.PageCount - 2;

            for (int i = startPage; i <= endPage; i++)
            {
                manager.CurrentPage = i;
                if (PdfRendererList.Any(x => !x.TryRenderPdf(manager)))
                    return false;
            }

            Logger.Info($"Success to render: {Position} for message: {manager.MessageId}", procName);
            return true;
        }
    }
}