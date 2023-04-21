using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfImageRenderer
{
    public class PutPdfImageRenderer : StoredProcedureBase
    {
        public PutPdfImageRenderer(Guid pdfRendererBaseId, byte sourceType, string imageSource)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@sourceType", sourceType);
            Parameters.Add("@imageSource", imageSource);
        }
    }
}