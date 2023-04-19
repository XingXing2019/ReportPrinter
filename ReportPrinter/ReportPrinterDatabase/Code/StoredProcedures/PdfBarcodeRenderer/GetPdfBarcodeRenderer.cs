using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer
{
    public class GetPdfBarcodeRenderer : StoredProcedureBase
    {
        public GetPdfBarcodeRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}