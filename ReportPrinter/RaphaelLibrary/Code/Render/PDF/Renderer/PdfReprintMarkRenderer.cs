using System;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfReprintMarkRenderer : PdfRendererBase
    {
        private string _text;
        private double _boardThickness;
        private Position _reprintMarkPosition;

        public PdfReprintMarkRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var text = node.SelectSingleNode(XmlElementHelper.S_TEXT)?.InnerText;
            if (string.IsNullOrEmpty(text))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TEXT, node, procName);
                return false;
            }
            _text = text;

            var boardThicknessStr = node.SelectSingleNode(XmlElementHelper.S_BOARD_THICKNESS)?.InnerText;
            if (!double.TryParse(boardThicknessStr, out var boardThickness))
            {
                boardThickness = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_BOARD_THICKNESS, boardThickness, procName);
            }
            _boardThickness = boardThickness;

            var reprintMarkPositionStr = node.SelectSingleNode(XmlElementHelper.S_REPRINT_MARK)?.InnerText;
            if (!Enum.TryParse(reprintMarkPositionStr, out Position reprintMarkPosition))
            {
                reprintMarkPosition = Position.Header;
                Logger.LogDefaultValue(node, XmlElementHelper.S_REPRINT_MARK, reprintMarkPosition, procName);
            }
            _reprintMarkPosition = reprintMarkPosition;

            Logger.Info($"Success to read ReprintMark, text:{_text}", procName);
            return true;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            var pdf = manager.Pdf;
            var page = pdf.Pages[0];
            using var graph = XGraphics.FromPdfPage(page);
            var textSize = graph.MeasureString(_text.Trim(), Font);

            if (!TryCalcRendererPosition(manager, textSize, _reprintMarkPosition))
                return false;

            RenderBoxModel(graph);
            RenderReprintMark(graph, _text.Trim(), _boardThickness);

            return true;
        }


        #region Helper

        private void RenderReprintMark(XGraphics graph, string text, double boardThickness)
        {
            var color = BrushColor.Color;
            var rect = new XRect(PaddingBox.X, PaddingBox.Y, PaddingBox.Width, PaddingBox.Height);

            var pen = new XPen(XColor.FromArgb((int)(Opacity * byte.MaxValue), color), boardThickness);
            graph.DrawRectangle(pen, rect);
            RenderText(graph, text);
        }

        #endregion
    }
}