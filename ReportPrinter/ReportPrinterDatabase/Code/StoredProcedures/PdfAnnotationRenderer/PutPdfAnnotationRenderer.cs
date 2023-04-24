using System;
using System.Data.SqlTypes;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfAnnotationRenderer
{
    public class PutPdfAnnotationRenderer : StoredProcedureBase
    {
        public PutPdfAnnotationRenderer(SqlGuid pdfRendererBaseId, byte annotationRendererType, string title, byte? icon, string content, Guid? sqlTemplateConfigSqlConfigId, string sqlResColumn)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@annotationRendererType", annotationRendererType);
            Parameters.Add("@title", title);
            Parameters.Add("@icon", icon);
            Parameters.Add("@content", content);
            Parameters.Add("@sqlTemplateConfigSqlConfigId", sqlTemplateConfigSqlConfigId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
        }
    }
}