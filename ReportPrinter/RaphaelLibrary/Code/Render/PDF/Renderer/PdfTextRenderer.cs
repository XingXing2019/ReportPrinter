using System;
using System.Xml;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfTextRenderer : PdfRendererBase
    {
        private TextRendererType _textRendererType;
        private string _content;

        private Sql _sql;
        private string _sqlResColumn;

        private string _mask;
        private string _title;

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

            if (textRendererType == TextRendererType.Text)
            {
                var content = node.SelectSingleNode(XmlElementHelper.S_CONTENT)?.InnerText;
                if (string.IsNullOrEmpty(content))
                {
                    Logger.LogMissingXmlLog(XmlElementHelper.S_CONTENT, node, procName);
                    return false;
                }
                _content = content;
            }
            else if (textRendererType == TextRendererType.Sql)
            {
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

                if (!SqlTemplateManager.Instance.TryGetSql(sqlTemplateId, sqlId, out var sql))
                {
                    return false;
                }
                _sql = sql;

                var sqlResColumn = node.SelectSingleNode(XmlElementHelper.S_SQL_RES_COLUMN)?.InnerText;
                if (string.IsNullOrEmpty(sqlResColumn))
                {
                    Logger.LogMissingXmlLog(XmlElementHelper.S_SQL_RES_COLUMN, node, procName);
                    return false;
                }
                _sqlResColumn = sqlResColumn;

                var sqlTitle = node.SelectSingleNode(XmlElementHelper.S_TITLE)?.InnerText;
                _title = sqlTitle;
            }
            else
            {
                var mask = node.SelectSingleNode(XmlElementHelper.S_Mask)?.InnerText;
                if (string.IsNullOrEmpty(mask))
                {
                    mask = "yyyy-MM-dd HH:mm:ss";
                    Logger.LogDefaultValue(XmlElementHelper.S_Mask, mask, procName);
                }
                _mask = mask;

                var timestampTitle = node.SelectSingleNode(XmlElementHelper.S_TITLE)?.InnerText;
                if (string.IsNullOrEmpty(timestampTitle))
                {
                    timestampTitle = "Print Date";
                    Logger.LogDefaultValue(XmlElementHelper.S_TITLE, timestampTitle, procName);
                }
                _title = timestampTitle;
            }

            Logger.Info($"Success to read Text with type of {_textRendererType}", procName);
            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfTextRenderer;
            if (_textRendererType == TextRendererType.Sql)
            {
                cloned._sql = this._sql.Clone() as Sql;
            }
            return cloned;
        }

        public override void RenderPdf(PdfDocumentManager manager)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum TextRendererType
    {
        Text,
        Sql,
        Timestamp
    }
}