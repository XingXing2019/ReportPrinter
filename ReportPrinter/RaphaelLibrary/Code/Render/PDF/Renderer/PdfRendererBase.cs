using System;
using System.Text;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public abstract class PdfRendererBase : IXmlReader
    {
        private PdfStructure _position;

        private MarginPaddingModel _margin;
        private MarginPaddingModel _padding;

        private HorizontalAlignment _horizontalAlignment;
        private VerticalAlignment _verticalAlignment;
        
        private int _row;
        private int _column;
        private int _rowSpan;
        private int _columnSpan;

        protected XFont Font;
        protected XSolidBrush BrushColor;
        protected XColor BackgroundColor;
        protected BoxModelParameter MarginBox;
        protected BoxModelParameter PaddingBox;
        protected BoxModelParameter ContentBox;

        protected PdfRendererBase(PdfStructure position)
        {
            _position = position;
        }

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";
            
            var marginStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_MARGIN);
            if (!LayoutHelper.TryCreateMarginPadding(marginStr, out var margin))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_MARGIN, "0", procName);
            }
            _margin = margin;

            var paddingStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PADDING);
            if (!LayoutHelper.TryCreateMarginPadding(paddingStr, out var padding))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_PADDING, "0", procName);
            }
            _padding = padding;

            var horizontalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HORIZONTAL_ALIGNMENT);
            if (!Enum.TryParse(horizontalAlignmentStr, out HorizontalAlignment horizontalAlignment))
            {
                horizontalAlignment = HorizontalAlignment.Center;
                Logger.LogDefaultValue(node, XmlElementHelper.S_HORIZONTAL_ALIGNMENT, horizontalAlignment, procName);
            }
            _horizontalAlignment = horizontalAlignment;

            var verticalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_VERTICAL_ALIGNMENT);
            if (!Enum.TryParse(verticalAlignmentStr, out VerticalAlignment verticalAlignment))
            {
                verticalAlignment = VerticalAlignment.Center;
                Logger.LogDefaultValue(node, XmlElementHelper.S_VERTICAL_ALIGNMENT, verticalAlignment, procName);
            }
            _verticalAlignment = verticalAlignment;

            var fontSizeStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_SIZE);
            if (!double.TryParse(fontSizeStr, out var fontSize))
            {
                fontSize = 8;
                Logger.LogDefaultValue(node, XmlElementHelper.S_FONT_SIZE, fontSize, procName);
            }

            var fontFamily = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_FAMILY);
            if (string.IsNullOrEmpty(fontFamily))
            {
                fontFamily = "Verdana";
                Logger.LogDefaultValue(node, XmlElementHelper.S_FONT_FAMILY, fontFamily, procName);
            }

            var fontStyleStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FONT_STYLE);
            if (!Enum.TryParse(fontStyleStr, out XFontStyle fontStyle))
            {
                fontStyle = XFontStyle.Regular;
                Logger.LogDefaultValue(node, XmlElementHelper.S_FONT_STYLE, fontStyle, procName);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var option = new XPdfFontOptions(PdfFontEncoding.Unicode);
            Font = new XFont(fontFamily, fontSize, fontStyle, option);

            var opacityStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_OPACITY);
            if (!double.TryParse(opacityStr, out var opacity))
            {
                opacity = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_OPACITY, opacity, procName);
            }

            var brushColorStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_BRUSH_COLOR);
            if (!ColorHelper.TryGenerateColor(brushColorStr, out var brushColor))
            {
                brushColor = XColors.Black;
                Logger.LogDefaultValue(node, XmlElementHelper.S_BRUSH_COLOR, brushColor, procName);
            }
            BrushColor = new XSolidBrush(XColor.FromArgb((int)(opacity * byte.MaxValue), brushColor));

            var backgroundColorStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_BACKGROUND_COLOR);
            if (!ColorHelper.TryGenerateColor(backgroundColorStr, out var backgroundColor))
            {
                backgroundColor = XColors.Transparent;
                Logger.LogDefaultValue(node, XmlElementHelper.S_BACKGROUND_COLOR, backgroundColor, procName);
            }
            BackgroundColor = backgroundColor;

            var rowStr = node.SelectSingleNode(XmlElementHelper.S_ROW)?.InnerText;
            if (!int.TryParse(rowStr, out var row))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ROW, node, procName);
                return false;
            }
            _row = row;

            var columnStr = node.SelectSingleNode(XmlElementHelper.S_COLUMN)?.InnerText;
            if (!int.TryParse(columnStr, out var column))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_COLUMN, node, procName);
                return false;
            }
            _column = column;

            var rowSpanStr = node.SelectSingleNode(XmlElementHelper.S_ROW_SPAN)?.InnerText;
            if (!int.TryParse(rowSpanStr, out var rowSpan))
            {
                rowSpan = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_ROW_SPAN, rowSpan, procName);
            }
            _rowSpan = rowSpan;

            var columnSpanStr = node.SelectSingleNode(XmlElementHelper.S_COLUMN_SPAN)?.InnerText;
            if (!int.TryParse(columnSpanStr, out var columnSpan))
            {
                columnSpan = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_COLUMN_SPAN, columnSpan, procName);
            }
            _columnSpan = columnSpan;

            return true;
        }

        public LayoutParameter GetLayoutParameter()
        {
            return new LayoutParameter
            {
                Margin = _margin,
                Padding = _padding,
                Position = _position,
                Row = _row,
                Column = _column,
                RowSpan = _rowSpan,
                ColumnSpan = _columnSpan,
                HorizontalAlignment = _horizontalAlignment,
                VerticalAlignment = _verticalAlignment
            };
        }

        public void SetMarginBox(BoxModelParameter marginBox)
        {
            MarginBox = marginBox;
        }

        public void SetPaddingBox(BoxModelParameter paddingBox)
        {
            PaddingBox = paddingBox;
        }

        public void SetContentBox(BoxModelParameter contentBox)
        {
            ContentBox = contentBox;
        }

        public virtual PdfRendererBase Clone()
        {
            return this.MemberwiseClone() as PdfRendererBase;
        }

        public abstract void RenderPdf(PdfDocumentManager manager);
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