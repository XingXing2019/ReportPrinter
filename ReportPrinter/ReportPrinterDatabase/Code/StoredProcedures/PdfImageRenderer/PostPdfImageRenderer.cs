using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfImageRenderer
{
    public class PostPdfImageRenderer : StoredProcedureBase
    {
        public PostPdfImageRenderer(Guid pdfRendererBaseId, byte sourceType, string imageSource)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@sourceType", sourceType);
            Parameters.Add("@imageSource", imageSource);
        }
    }
}