using System;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfAnnotationRenderer : PdfRendererBase
    {
        private AnnotationRendererType _annotationRendererType;
        private string _content;

        private Sql _sql;
        private SqlResColumn _sqlResColumn;

        private string _title;
        private PdfTextAnnotationIcon _icon;

        public PdfAnnotationRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var typeStr = node.SelectSingleNode(XmlElementHelper.S_TYPE)?.InnerText;
            if (!Enum.TryParse(typeStr, out AnnotationRendererType annotationRendererType))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TYPE, node, procName);
                return false;
            }
            _annotationRendererType = annotationRendererType;

            var title = node.SelectSingleNode(XmlElementHelper.S_TITLE)?.InnerText;
            _title = title;

            var iconStr = node.SelectSingleNode(XmlElementHelper.S_ICON)?.InnerText;
            if (!Enum.TryParse(iconStr, out PdfTextAnnotationIcon icon))
            {
                icon = PdfTextAnnotationIcon.NoIcon;
                Logger.LogDefaultValue(node, XmlElementHelper.S_ICON, icon, procName);
            }
            _icon = icon;

            if (_annotationRendererType == AnnotationRendererType.Text)
            {
                if (!TryReadContent(node, procName, out var content))
                    return false;
                
                _content = content;
                Logger.Info($"Success to read Annotation with type of {_annotationRendererType}, content: {_content}", procName);
            }
            else if (_annotationRendererType == AnnotationRendererType.Sql)
            {
                if (!TryReadSql(node, procName, out var sql, out var sqlResColumnList))
                    return false;

                if (sqlResColumnList.Count != 1)
                {
                    Logger.Error($"{this.GetType().Name} cna only have one sql resutle column", procName);
                    return false;
                }

                _sql = sql;
                _sqlResColumn = sqlResColumnList[0];
                Logger.Info($"Success to read Annotation with type of {_annotationRendererType}, sql id: {_sql.Id}, res column: {_sqlResColumn}", procName);
            }

            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfAnnotationRenderer;
            if (_annotationRendererType == AnnotationRendererType.Sql)
            {
                cloned._sql = this._sql.Clone() as Sql;
                cloned._sqlResColumn = this._sqlResColumn.Clone();
            }
            return cloned;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            var pdf = manager.Pdf;
            var page = pdf.Pages[manager.CurrentPage];
            using var graph = XGraphics.FromPdfPage(page);
            RenderBoxModel(graph);

            if (_annotationRendererType == AnnotationRendererType.Sql)
            {
                if (!_sql.TryExecute(manager.MessageId, _sqlResColumn, out var res))
                    return false;

                _content = res;
            }

            RenderAnnotation(graph, page);
            return true;
        }


        #region Helper

        private void RenderAnnotation(XGraphics graph, PdfPage page)
        {
            var annotation = new PdfTextAnnotation
            {
                Title = _title,
                Contents = _content,
                Icon = _icon,
                Color = BrushColor.Color,
                Opacity = Opacity
            };

            var rect = new XRect(ContentBox.X, ContentBox.Y, ContentBox.Width, ContentBox.Height);
            rect = graph.Transformer.WorldToDefaultPage(rect);
            annotation.Rectangle = new PdfRectangle(rect);
            page.Annotations.Add(annotation);
        }

        #endregion
    }

    public enum AnnotationRendererType
    {
        Text,
        Sql
    }
}