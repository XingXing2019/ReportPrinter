using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfTextRenderer
{
    public class PutPdfTextRenderer : StoredProcedureBase
    {
        public PutPdfTextRenderer(Guid pdfRendererBaseId, byte textRendererType, string content, Guid? sqlTemplateConfigSqlConfigId, string sqlResColumn, string mask, string title)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@textRendererType", textRendererType);
            Parameters.Add("@content", content);
            Parameters.Add("@sqlTemplateConfigSqlConfigId", sqlTemplateConfigSqlConfigId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
            Parameters.Add("@mask", mask);
            Parameters.Add("@title", title);
        }
    }
}