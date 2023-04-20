using System.Data.SqlTypes;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfAnnotationRenderer
{
    public class PostPdfAnnotationRenderer : StoredProcedureBase
    {
        public PostPdfAnnotationRenderer(SqlGuid pdfRendererBaseId, byte annotationRendererType, string title, byte? icon, string content, string sqlTemplateId, string sqlId, string sqlResColumn)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@annotationRendererType", annotationRendererType);
            Parameters.Add("@title", title);
            Parameters.Add("@icon", icon);
            Parameters.Add("@content", content);
            Parameters.Add("@sqlTemplateId", sqlTemplateId);
            Parameters.Add("@sqlId", sqlId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
        }
    }
}