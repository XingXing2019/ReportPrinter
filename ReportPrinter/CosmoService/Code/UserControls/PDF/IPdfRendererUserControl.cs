using ReportPrinterDatabase.Code.Model;

namespace CosmoService.Code.UserControls.PDF
{
    public interface IPdfRendererUserControl
    {
        bool ValidateInput();
        void Save(PdfRendererBaseModel rendererBase);
    }
}