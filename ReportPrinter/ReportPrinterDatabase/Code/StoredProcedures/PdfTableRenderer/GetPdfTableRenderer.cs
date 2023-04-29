using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfTableRenderer
{
    public class GetPdfTableRenderer : StoredProcedureBase
    {
        public GetPdfTableRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}