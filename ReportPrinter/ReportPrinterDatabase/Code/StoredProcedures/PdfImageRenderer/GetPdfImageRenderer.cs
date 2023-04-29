using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfImageRenderer
{
    public class GetPdfImageRenderer : StoredProcedureBase
    {
        public GetPdfImageRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}