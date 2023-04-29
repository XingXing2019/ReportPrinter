using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfReprintMarkRenderer
{
    public class PutPdfReprintMarkRenderer : StoredProcedureBase
    {
        public PutPdfReprintMarkRenderer(Guid pdfRendererBaseId, string text, double? boardThickness, byte? location)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@text", text);
            Parameters.Add("@boardThickness", boardThickness);
            Parameters.Add("@location", location);
        }
    }
}