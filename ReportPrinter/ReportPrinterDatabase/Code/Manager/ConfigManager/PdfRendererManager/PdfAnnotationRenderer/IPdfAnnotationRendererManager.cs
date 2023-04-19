using ReportPrinterDatabase.Code.Model;
using System.Threading.Tasks;
using System;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public interface IPdfAnnotationRendererManager
    {
        Task Post(PdfAnnotationRendererModel annotationRenderer);
        Task<PdfAnnotationRendererModel> Get(Guid pdfRendererBaseId);
        Task PutPdfAnnotationRenderer(PdfAnnotationRendererModel annotationRenderer);
    }
}