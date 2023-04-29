using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfTextRenderer
{
    public class GetPdfTextRenderer : StoredProcedureBase
    {
        public GetPdfTextRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}