using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfAnnotationRenderer
{
    public class GetPdfAnnotationRenderer : StoredProcedureBase
    {
        public GetPdfAnnotationRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}