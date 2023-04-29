using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfTableRenderer
{
    public class PostPdfTableRenderer : StoredProcedureBase
    {
        public PostPdfTableRenderer(Guid pdfRendererBaseId, double? boardThickness, double? lineSpace, byte? titleHorizontalAlignment, bool? hideTitle, double? space, byte? titleColor, double? titleColorOpacity, Guid sqlTemplateConfigSqlConfigId, string sqlVariable, Guid? subPdfTableRendererId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@boardThickness", boardThickness);
            Parameters.Add("@lineSpace", lineSpace);
            Parameters.Add("@titleHorizontalAlignment", titleHorizontalAlignment);
            Parameters.Add("@hideTitle", hideTitle);
            Parameters.Add("@space", space);
            Parameters.Add("@titleColor", titleColor);
            Parameters.Add("@titleColorOpacity", titleColorOpacity);
            Parameters.Add("@sqlTemplateConfigSqlConfigId", sqlTemplateConfigSqlConfigId);
            Parameters.Add("@sqlVariable", sqlVariable);
            Parameters.Add("@subPdfTableRendererId", subPdfTableRendererId);
        }
    }
}