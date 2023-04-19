using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public class PdfAnnotationRendererSPManager : IPdfAnnotationRendererManager
    {
        public Task Post(PdfAnnotationRendererModel annotationRenderer)
        {
            throw new NotImplementedException();
        }

        public Task<PdfAnnotationRendererModel> Get(Guid pdfRendererBaseId)
        {
            throw new NotImplementedException();
        }

        public Task PutPdfAnnotationRenderer(PdfAnnotationRendererModel annotationRenderer)
        {
            throw new NotImplementedException();
        }
    }
}