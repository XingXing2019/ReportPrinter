﻿using System;
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
        private bool _hideTitle;

        private Sql _sql;
        private List<SqlResColumn> _sqlResColumnList;

        private Dictionary<SqlResColumn, BoxModel> _columnPositions;

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

            var hideTitleStr = node.SelectSingleNode(XmlElementHelper.S_HIDE_TITLE)?.InnerText;
            if (!bool.TryParse(hideTitleStr, out var hideTitle))
            {
                hideTitle = false;
                Logger.LogDefaultValue(node, XmlElementHelper.S_HIDE_TITLE, hideTitle, procName);
            }
            _hideTitle = hideTitle;

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

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            if (!_sql.TryExecute(manager.MessageId, _sqlResColumnList, out var res))
                return false;

            _columnPositions = CalcPosition(manager);
            
            RenderTableTitle(manager);
            RenderTableContent(manager, res);

            return true;
        }


        #region Helper
        
        private void RenderTableTitle(PdfDocumentManager manager)
        {
            if (_hideTitle) return;

            var pdf = manager.Pdf;
            var page = pdf.Pages[^1];
            var container = manager.PageBodyContainer;

            using var graph = XGraphics.FromPdfPage(page);
            var pen = new XPen(BrushColor.Color, _boardThickness);
            graph.DrawLine(pen, container.LeftBoundary, manager.YCursor, container.RightBoundary, manager.YCursor);

            var textSize = CalcTextSize(graph);
            var lineSpace = textSize.Height * _lineSpace;

            manager.YCursor += lineSpace;

            var maxLineCount = 0;
            foreach (var column in _columnPositions.Keys)
            {
                var position = _columnPositions[column];
                var textPosition = CalcTextPosition(_titleHorizontalAlignment);

                var lines = LayoutHelper.AllocateWords(column.Title, textSize.WidthPerLetter, position.Width);
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

        private void RenderTableContent(PdfDocumentManager manager, List<Dictionary<string, string>> sqlRes)
        {
            var pdf = manager.Pdf;
            var graph = XGraphics.FromPdfPage(pdf.Pages[^1]);

            var pen = new XPen(BrushColor.Color, _boardThickness);
            var container = manager.PageBodyContainer;

            var textSize = CalcTextSize(graph);
            var lineSpace = _lineSpace * textSize.Height;

            foreach (var row in sqlRes)
            {
                var maxLineCount = 0;
                foreach (var column in _columnPositions.Keys)
                {
                    var position = _columnPositions[column];
                    var lines = LayoutHelper.AllocateWords(row[column.Id], textSize.WidthPerLetter, position.Width);
                    maxLineCount = Math.Max(maxLineCount, lines.Count);
                }

                if (manager.YCursor + textSize.Height * maxLineCount + lineSpace > manager.BottomBoundary)
                {
                    graph.DrawLine(pen, container.LeftBoundary, manager.BottomBoundary, container.RightBoundary, manager.BottomBoundary);
                    graph.Dispose();
                    manager.AddPage(RenderTableTitle);
                    graph = XGraphics.FromPdfPage(pdf.Pages[^1]);
                }

                foreach (var column in _columnPositions.Keys)
                {
                    var position = _columnPositions[column];
                    var textPosition = CalcTextPosition(HorizontalAlignment);
                    
                    var lines = LayoutHelper.AllocateWords(row[column.Id], textSize.WidthPerLetter, position.Width);
                    var y = manager.YCursor;

                    foreach (var line in lines)
                    {
                        var rect = new XRect(position.X, manager.YCursor, position.Width, textSize.Height);
                        graph.DrawString(line, Font, BrushColor, rect, textPosition);
                        manager.YCursor += textSize.Height;
                    }

                    manager.YCursor = y;
                }

                manager.YCursor += textSize.Height * maxLineCount + lineSpace;
            }

            graph.DrawLine(pen, container.LeftBoundary, container.LastPageBottomBoundary, container.RightBoundary, container.LastPageBottomBoundary);
            graph.Dispose();
        }

        private Dictionary<SqlResColumn, BoxModel> CalcPosition(PdfDocumentManager manager)
        {
            var container = manager.PageBodyContainer;
            var totalWidth = container.RightBoundary - container.LeftBoundary;

            var res = new Dictionary<SqlResColumn, BoxModel>();
            var x = container.LeftBoundary;
            foreach (var sqlResColumn in _sqlResColumnList)
            {
                var width = totalWidth * sqlResColumn.WidthRatio;
                res.Add(sqlResColumn, new BoxModel(x, -1, width, -1));
                x += width;
            }

            return res;
        }

        private XStringFormat CalcTextPosition(HorizontalAlignment horizontalAlignment)
        {
            if (horizontalAlignment == HorizontalAlignment.Left)
                return XStringFormats.TopLeft;
            else if (horizontalAlignment == HorizontalAlignment.Center)
                return XStringFormats.TopCenter;
            else
                return XStringFormats.TopRight;
        }

        private TextSize CalcTextSize(XGraphics graph)
        {
            var text = "dummy";
            var textSize = graph.MeasureString(text, Font);
            var widthPerLetter = textSize.Width / text.Length;
            return new TextSize(textSize.Width, textSize.Height, widthPerLetter);
        }
        
        #endregion
    }
}