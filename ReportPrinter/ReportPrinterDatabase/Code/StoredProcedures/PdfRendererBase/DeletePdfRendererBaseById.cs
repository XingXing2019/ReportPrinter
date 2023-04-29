using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class DeletePdfRendererBaseById : StoredProcedureBase
    {
        public DeletePdfRendererBaseById(Guid pdfRendererId)
        {
            Parameters.Add("@pdfRendererId", pdfRendererId);
        }
    }
}