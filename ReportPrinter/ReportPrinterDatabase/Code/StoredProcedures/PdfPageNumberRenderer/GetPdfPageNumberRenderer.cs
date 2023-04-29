using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfPageNumberRenderer
{
    public class GetPdfPageNumberRenderer : StoredProcedureBase
    {
        public GetPdfPageNumberRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}