using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class GetPdfRendererBase : StoredProcedureBase
    {
        public GetPdfRendererBase(Guid pdfRendererId)
        {
            Parameters.Add("@pdfRendererId", pdfRendererId);
        }
    }
}