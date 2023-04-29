using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class PutPdfRendererBase : StoredProcedureBase
    {
        public PutPdfRendererBase(Guid pdfRendererBaseId, string id, byte rendererType, string margin, string padding, byte? horizontalAlignment,
            byte? verticalAlignment, byte? position, double? left, double? right, double? top, double? bottom, double? fontSize, string fontFamily, byte? fontStyle,
            double? opacity, byte? brushColor, byte? backgroundColor, int row, int column, int? rowSpan, int? columnSpan)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@id", id);
            Parameters.Add("@rendererType", rendererType);
            Parameters.Add("@margin", margin);
            Parameters.Add("@padding", padding);
            Parameters.Add("@horizontalAlignment", horizontalAlignment);
            Parameters.Add("@verticalAlignment", verticalAlignment);
            Parameters.Add("@position", position);
            Parameters.Add("@left", left);
            Parameters.Add("@right", right);
            Parameters.Add("@top", top);
            Parameters.Add("@bottom", bottom);
            Parameters.Add("@fontSize", fontSize);
            Parameters.Add("@fontFamily", fontFamily);
            Parameters.Add("@fontStyle", fontStyle);
            Parameters.Add("@opacity", opacity);
            Parameters.Add("@brushColor", brushColor);
            Parameters.Add("@backgroundColor", backgroundColor);
            Parameters.Add("@row", row);
            Parameters.Add("@column", column);
            Parameters.Add("@rowSpan", rowSpan);
            Parameters.Add("@columnSpan", columnSpan);
        }
    }
}