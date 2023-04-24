using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfWaterMarkRenderer
{
    public class PutPdfWaterMarkRenderer : StoredProcedureBase
    {
        public PutPdfWaterMarkRenderer(Guid pdfRendererBaseId, byte waterMarkType, string content, byte? location, Guid? sqlTemplateConfigSqlConfigId, string sqlResColumn, int? startPage, int? endPage, double? rotate)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@waterMarkType", waterMarkType);
            Parameters.Add("@content", content);
            Parameters.Add("@location", location);
            Parameters.Add("@sqlTemplateConfigSqlConfigId", sqlTemplateConfigSqlConfigId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
            Parameters.Add("@startPage", startPage);
            Parameters.Add("@endPage", endPage);
            Parameters.Add("@rotate", rotate);
        }
    }
}