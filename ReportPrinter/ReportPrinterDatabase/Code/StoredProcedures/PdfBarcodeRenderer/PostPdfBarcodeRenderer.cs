using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer
{
    public class PostPdfBarcodeRenderer : StoredProcedureBase
    {
        public PostPdfBarcodeRenderer(Guid pdfRendererBaseId, int? barcodeFormat, bool showBarcodeText, string sqlTemplateId, string sqlId, string sqlResColumn)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@barcodeFormat", barcodeFormat);
            Parameters.Add("@showBarcodeText", showBarcodeText);
            Parameters.Add("@sqlTemplateId", sqlTemplateId);
            Parameters.Add("@sqlId", sqlId);
            Parameters.Add("@sqlResColumn", sqlResColumn);
        }
    }
}