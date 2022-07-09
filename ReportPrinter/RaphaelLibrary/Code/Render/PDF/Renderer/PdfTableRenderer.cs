using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PdfTableRenderer : PdfRendererBase
    {

        private double _boardThickness;
        private double _lineSpace;
        private HorizontalAlignment _titleHorizontalAlignment;

        private Sql _sql;
        private List<SqlResColumn> _sqlResColumnList;

        public PdfTableRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var boardThicknessStr = node.SelectSingleNode(XmlElementHelper.S_BOARD_THICKNESS)?.InnerText;
            if (!double.TryParse(boardThicknessStr, out var boardThickness))
            {
                boardThickness = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_BOARD_THICKNESS, boardThickness, procName);
            }
            _boardThickness = boardThickness;

            var lineSpaceStr = node.SelectSingleNode(XmlElementHelper.S_LINE_SPACE)?.InnerText;
            if (!double.TryParse(lineSpaceStr, out var lineSpace))
            {
                lineSpace = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_LINE_SPACE, lineSpace, procName);
            }
            _lineSpace = lineSpace;

            var titleHorizontalAlignmentStr = node.SelectSingleNode(XmlElementHelper.S_TITLE_HORIZONTAL_ALIGNMENT)?.InnerText;
            if (!Enum.TryParse(titleHorizontalAlignmentStr, out HorizontalAlignment titleHorizontalAlignment))
            {
                titleHorizontalAlignment = HorizontalAlignment.Left;
                Logger.LogDefaultValue(node, XmlElementHelper.S_TITLE_HORIZONTAL_ALIGNMENT, titleHorizontalAlignment, procName);
            }
            _titleHorizontalAlignment = titleHorizontalAlignment;

            if (!TryReadSql(node, procName, out var sql, out var sqlResColumnList))
            {
                return false;
            }

            var uniqueId = new HashSet<string>(sqlResColumnList.Select(x => x.Id));
            if (uniqueId.Count != sqlResColumnList.Count)
            {
                Logger.Error($"Duplicate sql result column id detected", procName);
                return false;
            }

            var totalWidthRatio = sqlResColumnList.Sum(x => x.WidthRatio);
            foreach (var sqlResColumn in sqlResColumnList)
            {
                sqlResColumn.WidthRatio /= totalWidthRatio;
            }

            _sql = sql;
            _sqlResColumnList = sqlResColumnList;
            
            Logger.Info($"Success to read Table, sql id: {_sql.Id}, result columns: {string.Join(',', _sqlResColumnList.Select(x => x.Id))}", procName);
            return true;
        }

        public override PdfRendererBase Clone()
        {
            var cloned = base.Clone() as PdfTableRenderer;
            cloned._sql = this._sql.Clone() as Sql;
            cloned._sqlResColumnList = this._sqlResColumnList.Select(x => x.Clone()).ToList();

            return cloned;
        }

        public override bool TryRenderPdf(PdfDocumentManager manager)
        {
            var renderName = this.GetType().Name;
            var procName = $"{renderName}.{nameof(TryRenderPdf)}";

            try
            {
                if (!_sql.TryExecute(manager.MessageId, _sqlResColumnList, out var res))
                    return false;

                var columnPositions = CalcPosition(manager);
                RenderTableTitle(manager, columnPositions);


                Logger.Info($"Success to render pdf: {renderName} for message: {manager.MessageId}", procName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during rendering pdf: {renderName} for message: {manager.MessageId}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, XGraphics graph, PdfPage page, string procName)
        {
            return true;
        }


        #region Helper

        private Dictionary<string, BoxModel> CalcPosition(PdfDocumentManager manager)
        {
            var container = manager.PageBodyContainer;
            var totalWidth = container.RightBoundary - container.LeftBoundary;

            var res = new Dictionary<string, BoxModel>();
            var x = container.LeftBoundary;
            foreach (var sqlResColumn in _sqlResColumnList)
            {
                var width = totalWidth * sqlResColumn.WidthRatio;
                res.Add(sqlResColumn.Title, new BoxModel(x, -1, width, -1));
                x += width;
            }

            return res;
        }

        private void RenderTableTitle(PdfDocumentManager manager, Dictionary<string, BoxModel> columnPositions)
        {
            var pdf = manager.Pdf;
            var page = pdf.Pages[^1];
            var container = manager.PageBodyContainer;

            using var graph = XGraphics.FromPdfPage(page);
            var pen = new XPen(BrushColor.Color, _boardThickness);
            graph.DrawLine(pen, container.LeftBoundary, manager.YCursor, container.RightBoundary, manager.YCursor);

            var text = "dummy";
            var textSize = graph.MeasureString(text, Font);
            
            var lineSpace = textSize.Height * _lineSpace;
            manager.YCursor += lineSpace;

            var maxLineCount = 0;
            foreach (var column in columnPositions.Keys)
            {
                var position = columnPositions[column];

                XStringFormat textPosition;
                if (_titleHorizontalAlignment == HorizontalAlignment.Left)
                    textPosition = XStringFormats.TopLeft;
                else if (_titleHorizontalAlignment == HorizontalAlignment.Center)
                    textPosition = XStringFormats.TopCenter;
                else
                    textPosition = XStringFormats.TopRight;

                var lines = LayoutHelper.AllocateWords(column, textSize.Width / text.Length, position.Width);
                var y = manager.YCursor;
                maxLineCount = Math.Max(maxLineCount, lines.Count);

                foreach (var line in lines)
                {
                    var rect = new XRect(position.X, manager.YCursor, position.Width, textSize.Height);
                    graph.DrawString(line, Font, BrushColor, rect, textPosition);
                    manager.YCursor += textSize.Height;
                }

                manager.YCursor = y;
            }

            manager.YCursor += textSize.Height * maxLineCount + lineSpace;
            graph.DrawLine(pen, container.LeftBoundary, manager.YCursor, container.RightBoundary, manager.YCursor);
            manager.YCursor += lineSpace;
        }

        #endregion
    }
}