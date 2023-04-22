using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfReprintMarkRenderer
{
    public class GetPdfReprintMarkRenderer : StoredProcedureBase
    {
        public GetPdfReprintMarkRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}