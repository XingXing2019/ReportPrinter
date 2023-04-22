using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfPageNumberRenderer
{
    public class PutPdfPageNumberRenderer : StoredProcedureBase
    {
        public PutPdfPageNumberRenderer(Guid pdfRendererBaseId, int? startPage, int? endPage, byte? pageNumberLocation)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@startPage", startPage);
            Parameters.Add("@endPage", endPage);
            Parameters.Add("@pageNumberLocation", pageNumberLocation);
        }
    }
}