using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer
{
    public interface IPdfBarcodeRendererManager
    {
        Task Post(PdfBarcodeRendererModel barcodeRenderer);
        Task<PdfBarcodeRendererModel> Get(Guid pdfRendererBaseId);
        Task PutPdfBarcodeRenderer(PdfBarcodeRendererModel barcodeRenderer);
    }
}