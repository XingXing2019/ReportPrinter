using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfWaterMarkRenderer
{
    public class GetPdfWaterMarkRenderer : StoredProcedureBase
    {
        public GetPdfWaterMarkRenderer(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}