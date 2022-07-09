using System;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfTextRenderer : PdfRendererBase
    {
        private TextRendererType _textRendererType;
        private string _content;

        private Sql _sql;
        private SqlResColumn _sqlResColumn;

        private string _mask;
        private string _title;

        public PdfTextRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var typeStr = node.SelectSingleNode(XmlElementHelper.S_TYPE)?.InnerText;
            if (!Enum.TryParse(typeStr, out TextRendererType textRendererType))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TYPE, node, procName);
                return false;
            }
            _textRendererType = textRendererType;

            if (_textRendererType == TextRendererType.Text)
            {
                if (!TryReadContent(node, procName, out var content))
                    return false;

                _content = content;
                Logger.Info($"Success to read Text with type of {_textRendererType}, content: {_content}", procName);
            }
            else if (_textRendererType == TextRendererType.Sql)
            {
                if (!TryReadSql(node, procName, out var sql, out var sqlResColumnList))
                    return false;

                _sql = sql;
                _sqlResColumn = sqlResColumnList[0];

                var sqlTitle = node.SelectSingleNode(XmlElementHelper.S_TITLE)?.InnerText;
                _title = sqlTitle;

                Logger.Info($"Success to read Text with type of {_textRendererType}, sql id: {_sql.Id}, res column: {_sqlResColumn}", procName);
            }
            else if (_textRendererType == TextRendererType.Timestamp)
            {
                var mask = node.SelectSingleNode(XmlElementHelper.S_Mask)?.InnerText;
                if (string.IsNullOrEmpty(mask))
                {
                    mask = "yyyy-MM-dd HH:mm:ss";
                    Logger.LogDefaultValue(node, XmlElementHelper.S_Mask, mask, procName);
                }
                _mask = mask;

                var timestampTitle = node.SelectSingleNode(XmlElementHelper.S_TITLE)?.InnerText;
                if (string.IsNullOrEmpty(timestampTitle))
                {
                    timestampTitle = "Print Date";
                    Logger.LogDefaultValue(node, XmlElementHelper.S_TITLE, timestampTitle, procName);
                }
                _title = timestampTitle;

                Logger.Info($"Success to read Text with type of {_textRendererType}", procName);
            }

            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfTextRenderer;
            if (_textRendererType == TextRendererType.Sql)
            {
                cloned._sql = this._sql.Clone() as Sql;
                cloned._sqlResColumn = this._sqlResColumn.Clone();
            }
            return cloned;
        }
        
        protected override bool TryPerformRender(PdfDocumentManager manager, XGraphics graph, PdfPage page, string procName)
        {
            if (_textRendererType == TextRendererType.Sql)
            {
                if (!_sql.TryExecute(manager.MessageId, _sqlResColumn, out var res))
                    return false;

                var title = string.IsNullOrEmpty(_title) ? string.Empty : $"{_title}: ";
                _content = $"{title}{res}";
            }
            else if (_textRendererType == TextRendererType.Timestamp)
            {
                _content = $"{_title}: {DateTime.Now.ToString(_mask)}";
            }

            RenderText(graph, _content.Trim());
            return true;
        }


        #region Helper

        private void RenderText(XGraphics graph, string text)
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

        #endregion
    }


    public enum TextRendererType
    {
        Text,
        Sql,
        Timestamp
    }
}