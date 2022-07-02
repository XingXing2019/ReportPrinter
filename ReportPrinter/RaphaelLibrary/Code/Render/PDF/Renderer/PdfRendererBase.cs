using RaphaelLibrary.Code.Render.PDF.Manager;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public abstract class PdfRendererBase
    {
        public abstract void RenderPdf(PdfDocumentManager manager);
    }
}