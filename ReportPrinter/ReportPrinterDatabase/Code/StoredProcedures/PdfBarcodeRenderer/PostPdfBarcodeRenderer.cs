using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer
{
    public class PostPdfBarcodeRenderer : StoredProcedureBase
    {
        public PostPdfBarcodeRenderer(Guid pdfRendererBaseId, int? barcodeFormat, bool showBarcodeText, Guid sqlTemplateConfigSqlConfigId, string sqlResColumn)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@barcodeFormat", barcodeFormat);
            Parameters.Add("@showBarcodeText", showBarcodeText);
            Parameters.Add("@sqlTemplateConfigSqlConfigId", sqlTemplateConfigSqlConfigId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
        }
    }
}