using System;
using System.Xml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfWaterMarkRenderer : PdfRendererBase
    {
        private WaterMarkRendererType _waterMarkType;
        private string _content;

        private Sql _sql;
        private SqlResColumn _sqlResColumn;

        private int _startPage;
        private int _endPage;
        private double? _rotate;

        public PdfWaterMarkRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var typeStr = node.SelectSingleNode(XmlElementHelper.S_TYPE)?.InnerText;
            if (!Enum.TryParse(typeStr, out WaterMarkRendererType waterMarkType))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TYPE, node, procName);
                return false;
            }
            _waterMarkType = waterMarkType;

            var startPageStr = node.SelectSingleNode(XmlElementHelper.S_START_PAGE)?.InnerText;
            if (!int.TryParse(startPageStr, out var startPage))
            {
                startPage = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_START_PAGE, startPage, procName);
            }
            _startPage = startPage - 1;

            var endPageStr = node.SelectSingleNode(XmlElementHelper.S_END_PAGE)?.InnerText;
            if (!int.TryParse(endPageStr, out var endPage))
            {
                endPage = -1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_END_PAGE, endPage, procName);
            }
            _endPage = endPage;

            var rotateStr = node.SelectSingleNode(XmlElementHelper.S_ROTATE)?.InnerText;
            if (!double.TryParse(rotateStr, out var rotate))
            {
                _rotate = null;
                Logger.LogDefaultValue(node, XmlElementHelper.S_ROTATE, _rotate, procName);
            }
            else
            {
                _rotate = rotate;
            }

            if (_waterMarkType == WaterMarkRendererType.Text)
            {
                if (!TryReadContent(node, procName, out var content))
                    return false;

                _content = content;
                Logger.Info($"Success to read WaterMark with type of {_waterMarkType}, content: {_content}, rotate: {_rotate}, start page: {_startPage}, end page: {_endPage}", procName);
            }
            else if (_waterMarkType == WaterMarkRendererType.Sql)
            {
                if (!TryReadSql(node, procName, out var sql, out var sqlResColumnList))
                    return false;

                if (sqlResColumnList.Count != 1)
                {
                    Logger.Error($"{this.GetType().Name} cna only have one sql result column", procName);
                    return false;
                }

                _sql = sql;
                _sqlResColumn = sqlResColumnList[0];
                Logger.Info($"Success to read Annotation with type of {_waterMarkType}, sql id: {_sql.Id}, res column: {_sqlResColumn}, rotate: {_rotate}, start page: {_startPage}, end page: {_endPage}", procName);
            }

            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfWaterMarkRenderer;
            if (_waterMarkType == WaterMarkRendererType.Sql)
            {
                cloned._sql = this._sql.Clone() as Sql;
            }
            return cloned;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            if (_waterMarkType == WaterMarkRendererType.Sql)
            {
                if (!_sql.TryExecute(manager.MessageId, _sqlResColumn, out var res))
                    return false;

                _content = res;
            }

            XSize textSize;
            var pdf = manager.Pdf;
            using (var graph = XGraphics.FromPdfPage(pdf.Pages[0]))
            {
                textSize = graph.MeasureString(_content, Font);
            }

            var x = manager.LeftBoundary;
            var y = manager.TopBoundary;
            var width = manager.RightBoundary - manager.LeftBoundary;
            var height = manager.BottomBoundary - manager.TopBoundary;

            var container = new BoxModel(x, y, width, height);
            if (!LayoutHelper.TryCreateMarginBox(container, textSize, this, out var marginBox))
                return false;
            MarginBox = marginBox;

            if (!LayoutHelper.TryCreatePaddingBox(container, textSize, this, out var paddingBox))
                return false;
            PaddingBox = paddingBox;

            if (!LayoutHelper.TryCreateContentBox(container, textSize, this, out var contentBox))
                return false;
            ContentBox = contentBox;

            var pageCount = pdf.PageCount;
            for (int i = _startPage; i <= pageCount + _endPage; i++)
            {
                using var graph = XGraphics.FromPdfPage(pdf.Pages[i]);
                RenderBoxModel(graph);
                RenderWaterMark(graph, pdf.Pages[i]);
            }

            return true;
        }


        #region Helper

        private void RenderWaterMark(XGraphics graph, PdfPage page)
        {
            graph.TranslateTransform(page.Width / 2, page.Height / 2);
            var angle = _rotate ?? -Math.Atan(page.Height / page.Width) * 180 / Math.PI;
            graph.RotateTransform(angle);
            graph.TranslateTransform(-page.Width / 2, -page.Height / 2);

            var rect = new XRect(ContentBox.X, ContentBox.Y, ContentBox.Width, ContentBox.Height);
            var format = new XStringFormat
            {
                Alignment = XStringAlignment.Near,
                LineAlignment = XLineAlignment.Near
            };
            graph.DrawString(_content, Font, BrushColor, rect, format);
        }

        #endregion
    }


    public enum WaterMarkRendererType
    {
        Text,
        Sql
    }
}