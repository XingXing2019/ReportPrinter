using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public abstract class PdfRendererBase : IXmlReader
    {
        private readonly PdfStructure _position;
        
        private int _row;
        private int _column;
        private int _rowSpan;
        private int _columnSpan;

        protected MarginPaddingModel Margin;
        protected MarginPaddingModel Padding;

        protected HorizontalAlignment HorizontalAlignment;
        protected VerticalAlignment VerticalAlignment;
        protected XFont Font;
        protected XSolidBrush BrushColor;
        protected double Opacity;
        protected XColor BackgroundColor;
        protected BoxModel MarginBox;
        protected BoxModel PaddingBox;
        protected BoxModel ContentBox;

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
            Margin = margin;

            var paddingStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PADDING);
            if (!LayoutHelper.TryCreateMarginPadding(paddingStr, out var padding))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_PADDING, "0", procName);
            }
            Padding = padding;

            var horizontalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HORIZONTAL_ALIGNMENT);
            if (!Enum.TryParse(horizontalAlignmentStr, out HorizontalAlignment horizontalAlignment))
            {
                horizontalAlignment = HorizontalAlignment.Center;
                Logger.LogDefaultValue(node, XmlElementHelper.S_HORIZONTAL_ALIGNMENT, horizontalAlignment, procName);
            }
            HorizontalAlignment = horizontalAlignment;

            var verticalAlignmentStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_VERTICAL_ALIGNMENT);
            if (!Enum.TryParse(verticalAlignmentStr, out VerticalAlignment verticalAlignment))
            {
                verticalAlignment = VerticalAlignment.Center;
                Logger.LogDefaultValue(node, XmlElementHelper.S_VERTICAL_ALIGNMENT, verticalAlignment, procName);
            }
            VerticalAlignment = verticalAlignment;

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
            Opacity = opacity;

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
            if (!int.TryParse(rowStr, out var row) && _position != PdfStructure.PdfPageBody)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ROW, node, procName);
                return false;
            }
            _row = row;

            var columnStr = node.SelectSingleNode(XmlElementHelper.S_COLUMN)?.InnerText;
            if (!int.TryParse(columnStr, out var column) && _position != PdfStructure.PdfPageBody)
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
                Margin = Margin,
                Padding = Padding,
                Position = _position,
                Row = _row,
                Column = _column,
                RowSpan = _rowSpan,
                ColumnSpan = _columnSpan
            };
        }

        public void SetMarginBox(BoxModel marginBox)
        {
            MarginBox = marginBox;
        }

        public void SetPaddingBox(BoxModel paddingBox)
        {
            PaddingBox = paddingBox;
        }

        public void SetContentBox(BoxModel contentBox)
        {
            ContentBox = contentBox;
        }

        public virtual PdfRendererBase Clone()
        {
            return this.MemberwiseClone() as PdfRendererBase;
        }

        public virtual bool TryRenderPdf(PdfDocumentManager manager)
        {
            var renderName = this.GetType().Name;
            var procName = $"{renderName}.{nameof(TryRenderPdf)}";

            try
            {
                if (!TryPerformRender(manager, procName))
                    return false;

                Logger.Info($"Success to render pdf: {renderName} for message: {manager.MessageId}", procName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during rendering pdf: {renderName} for message: {manager.MessageId}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        protected abstract bool TryPerformRender(PdfDocumentManager manager, string procName);

        protected bool TryReadContent(XmlNode node, string procName, out string content)
        {
            content = node.SelectSingleNode(XmlElementHelper.S_CONTENT)?.InnerText;
            if (string.IsNullOrEmpty(content))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_CONTENT, node, procName);
                return false;
            }

            return true;
        }

        protected bool TryReadSql(XmlNode node, string procName, out Sql sql, out List<SqlResColumn> sqlResColumnList)
        {
            sql = null;
            sqlResColumnList = new List<SqlResColumn>();

            var sqlTemplateId = node.SelectSingleNode(XmlElementHelper.S_SQL_TEMPLATE_ID)?.InnerText;
            if (string.IsNullOrEmpty(sqlTemplateId))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SQL_TEMPLATE_ID, node, procName);
                return false;
            }

            var sqlId = node.SelectSingleNode(XmlElementHelper.S_SQL_ID)?.InnerText;
            if (string.IsNullOrEmpty(sqlId))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SQL_ID, node, procName);
                return false;
            }

            if (!SqlTemplateManager.Instance.TryGetSql(sqlTemplateId, sqlId, out sql))
            {
                return false;
            }

            var sqlResColumns = node.SelectNodes(XmlElementHelper.S_SQL_RES_COLUMN);
            foreach (XmlNode sqlResColumnNode in sqlResColumns)
            {
                var sqlResColumn = new SqlResColumn();
                if (!sqlResColumn.ReadXml(sqlResColumnNode))
                    return false;

                sqlResColumnList.Add(sqlResColumn);
            }

            if (sqlResColumnList.Count == 0)
            {
                Logger.Error($"Unable to read any valid sql result column", procName);
                return false;
            }

            return true;
        }
        
        protected void RenderBoxModel(XGraphics graph)
        {
            if (BackgroundColor == XColors.Transparent)
                return;

            var options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            var font = new XFont("Verdana", 4, XFontStyle.Regular, options);
            var brush = new XSolidBrush(XColors.White);

            var marginBox = RenderBox(graph, MarginBox, 0.3);
            var paddingBox = RenderBox(graph, PaddingBox, 0.4);
            var contentBox = RenderBox(graph, ContentBox, 1);

            RenderSize(graph, font, brush, marginBox, Margin);
            RenderSize(graph, font, brush, paddingBox, Padding);
        }

        protected void RenderText(XGraphics graph, string text)
        {
            XStringFormat position;
            if (VerticalAlignment == VerticalAlignment.Top)
            {
                if (HorizontalAlignment == HorizontalAlignment.Left)
                    position = XStringFormats.TopLeft;
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                    position = XStringFormats.TopCenter;
                else
                    position = XStringFormats.TopRight;

            }
            else if (VerticalAlignment == VerticalAlignment.Center)
            {
                if (HorizontalAlignment == HorizontalAlignment.Left)
                    position = XStringFormats.CenterLeft;
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                    position = XStringFormats.Center;
                else
                    position = XStringFormats.CenterRight;
            }
            else
            {
                if (HorizontalAlignment == HorizontalAlignment.Left)
                    position = XStringFormats.BottomLeft;
                else if (HorizontalAlignment == HorizontalAlignment.Center)
                    position = XStringFormats.BottomCenter;
                else
                    position = XStringFormats.BottomRight;
            }

            var rect = new XRect(ContentBox.X, ContentBox.Y, ContentBox.Width, ContentBox.Height);
            graph.DrawString(text, Font, BrushColor, rect, position);
        }
        

        #region Helper

        private XRect RenderBox(XGraphics graph, BoxModel box, double opacity)
        {
            var rect = new XRect(box.X, box.Y, box.Width, box.Height);
            var brush = new XSolidBrush(XColor.FromArgb((int)(opacity * byte.MaxValue), BackgroundColor));
            graph.DrawRectangle(brush, rect);
            return rect;
        }

        private void RenderSize(XGraphics graph, XFont font, XBrush brush, XRect rect, MarginPaddingModel size)
        {
            graph.DrawString(size.Left.ToString(), font, brush, rect, XStringFormats.CenterLeft);
            graph.DrawString(size.Right.ToString(), font, brush, rect, XStringFormats.CenterRight);
            graph.DrawString(size.Top.ToString(), font, brush, rect, XStringFormats.TopCenter);
            graph.DrawString(size.Bottom.ToString(), font, brush, rect, XStringFormats.BottomCenter);
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