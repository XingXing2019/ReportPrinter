using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfWaterMarkRenderer
{
    public class PutPdfWaterMarkRenderer : StoredProcedureBase
    {
        public PutPdfWaterMarkRenderer(Guid pdfRendererBaseId, byte waterMarkType, string content, byte? location, string sqlTemplateId, string sqlId, string sqlResColumn, int? startPage, int? endPage, double? rotate)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@waterMarkType", waterMarkType);
            Parameters.Add("@content", content);
            Parameters.Add("@location", location);
            Parameters.Add("@sqlTemplateId", sqlTemplateId);
            Parameters.Add("@sqlId", sqlId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
            Parameters.Add("@startPage", startPage);
            Parameters.Add("@endPage", endPage);
            Parameters.Add("@rotate", rotate);
        }
    }
}