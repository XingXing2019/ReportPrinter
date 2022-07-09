using System.Collections.Generic;
using System.Linq;
using RaphaelLibrary.Code.Render.PDF.Manager;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public class PdfPageHeader : PdfStructureBase
    {
        public PdfPageHeader(HashSet<string> rendererNames) 
            : base(PdfStructure.PdfPageHeader, rendererNames) { }

        public override bool TryRenderPdfStructure(PdfDocumentManager manager)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryRenderPdfStructure)}";

            int startPage = 1, endPage = manager.Pdf.PageCount - 1;

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