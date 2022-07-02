using System;
using System.Text;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public abstract class PdfRendererBase : IXmlReader
    {
        protected MarginPaddingModel Margin;
        protected MarginPaddingModel Padding;

        protected HorizontalAlignment HorizontalAlignment;
        protected VerticalAlignment VerticalAlignment;

        protected XFont Font;
        protected XSolidBrush BrushColor;

        protected int Row;
        protected int Column;
        protected int RowSpan;
        protected int ColumnSpan;

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var marginStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_MARGIN);
            if (!TryCreateMarginPadding(marginStr, out var margin))
            {
                Logger.LogDefaultValue(XmlElementHelper.S_MARGIN, "0", procName);
            }
            Margin = margin;

            var paddingStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PADDING);
            if (!TryCreateMarginPadding(paddingStr, out var padding))
            {
                Logger.LogDefaultValue(XmlElementHelper.S_PADDING, "0", procName);
            }
            Padding = padding;

            var horizontalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HORIZONTAL_ALIGNMENT);
            if (!Enum.TryParse(horizontalAlignmentStr, out HorizontalAlignment horizontalAlignment))
            {
                horizontalAlignment = HorizontalAlignment.Center;
                Logger.LogDefaultValue(XmlElementHelper.S_HORIZONTAL_ALIGNMENT, horizontalAlignment, procName);
            }
            HorizontalAlignment = horizontalAlignment;

            var verticalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_VERTICAL_ALIGNMENT);
            if (!Enum.TryParse(verticalAlignmentStr, out VerticalAlignment verticalAlignment))
            {
                verticalAlignment = VerticalAlignment.Center;
                Logger.LogDefaultValue(XmlElementHelper.S_VERTICAL_ALIGNMENT, verticalAlignment, procName);
            }
            VerticalAlignment = verticalAlignment;

            var fontSizeStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_SIZE);
            if (!double.TryParse(fontSizeStr, out var fontSize))
            {
                fontSize = 8;
                Logger.LogDefaultValue(XmlElementHelper.S_FONT_SIZE, fontSize, procName);
            }

            var fontFamily = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_FAMILY);
            if (string.IsNullOrEmpty(fontFamily))
            {
                fontFamily = "Verdana";
                Logger.LogDefaultValue(XmlElementHelper.S_FONT_FAMILY, fontFamily, procName);
            }

            var fontStyleStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_STYLE);
            if (!Enum.TryParse(fontStyleStr, out XFontStyle fontStyle))
            {
                fontStyle = XFontStyle.Regular;
                Logger.LogDefaultValue(XmlElementHelper.S_FONT_STYLE, fontStyle, procName);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var option = new XPdfFontOptions(PdfFontEncoding.Unicode);
            Font = new XFont(fontFamily, fontSize, fontStyle, option);

            var opacityStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_OPACITY);
            if (!double.TryParse(opacityStr, out var opacity))
            {
                opacity = 1;
                Logger.LogDefaultValue(XmlElementHelper.S_OPACITY, opacity, procName);
            }

            var brushColorStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_BRUSH_COLOR);
            if (!ColorHelper.TryGenerateColor(brushColorStr, out var color))
            {
                color = XColors.Black;
                Logger.LogDefaultValue(XmlElementHelper.S_BRUSH_COLOR, color, procName);
            }
            BrushColor = new XSolidBrush(XColor.FromArgb((int)(opacity * byte.MaxValue), color));

            var rowStr = node.SelectSingleNode(XmlElementHelper.S_ROW)?.InnerText;
            if (!int.TryParse(rowStr, out var row))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ROW, node, procName);
                return false;
            }
            Row = row;

            var columnStr = node.SelectSingleNode(XmlElementHelper.S_COLUMN)?.InnerText;
            if (!int.TryParse(columnStr, out var column))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_COLUMN, node, procName);
                return false;
            }
            Column = column;

            var rowSpanStr = node.SelectSingleNode(XmlElementHelper.S_ROW_SPAN)?.InnerText;
            if (!int.TryParse(rowSpanStr, out var rowSpan))
            {
                rowSpan = 1;
                Logger.LogDefaultValue(XmlElementHelper.S_ROW_SPAN, rowSpan, procName);
            }
            RowSpan = rowSpan;

            var columnSpanStr = node.SelectSingleNode(XmlElementHelper.S_COLUMN_SPAN)?.InnerText;
            if (!int.TryParse(columnSpanStr, out var columnSpan))
            {
                columnSpan = 1;
                Logger.LogDefaultValue(XmlElementHelper.S_COLUMN_SPAN, columnSpan, procName);
            }
            ColumnSpan = columnSpan;

            return true;
        }

        public virtual PdfRendererBase Clone()
        {
            return this.MemberwiseClone() as PdfRendererBase;
        }

        public abstract void RenderPdf(PdfDocumentManager manager);

        #region Helper

        private bool TryCreateMarginPadding(string input, out MarginPaddingModel model)
        {
            try
            {
                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var top = double.Parse(parts[0]);
                var right = double.Parse(parts[1]);
                var bottom = double.Parse(parts[2]);
                var left = double.Parse(parts[3]);
                model = new MarginPaddingModel(top, right, bottom, left);
                return true;
            }
            catch
            {
                model = new MarginPaddingModel(0, 0, 0, 0);
                return false;
            }
        }

        #endregion
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }
}