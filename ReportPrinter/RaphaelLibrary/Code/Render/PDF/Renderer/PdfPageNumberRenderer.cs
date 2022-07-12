using System;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfPageNumberRenderer : PdfRendererBase
    {
        private int _startPage;
        private int _endPage;
        private PageNumberPosition _pageNumberPosition;

        public PdfPageNumberRenderer(PdfStructure position) : base(position) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var pageNumberPositionStr = node.SelectSingleNode(XmlElementHelper.S_POSITION)?.InnerText;
            if (!Enum.TryParse(pageNumberPositionStr, out PageNumberPosition pageNumberPosition))
            {
                pageNumberPosition = PageNumberPosition.Footer;
                Logger.LogDefaultValue(node, XmlElementHelper.S_POSITION, pageNumberPosition, procName);
            }
            _pageNumberPosition = pageNumberPosition;

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
            
            Logger.Info($"Success to read PageNumber, start page: {_startPage}, end page: {_endPage}", procName);
            return true;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            if (!TryCalcRendererPosition(manager))
                return false;

            var pdf = manager.Pdf;
            var outline = pdf.Outlines;
            for (int i = 0; i < pdf.PageCount; i++)
            {
                if (i < _startPage || i > pdf.PageCount + _endPage)
                    continue;

                var text = $"Page {i + 1} of {pdf.PageCount}";
                using var graph = XGraphics.FromPdfPage(pdf.Pages[i]);
                RenderBoxModel(graph);
                RenderText(graph, text);
                outline.Add(text, pdf.Pages[i], true);
            }

            return true;
        }


        #region Helper

        private bool TryCalcRendererPosition(PdfDocumentManager manager)
        {
            var pdf = manager.Pdf;

            BoxModel container;
            if (_pageNumberPosition == PageNumberPosition.Header)
            {
                var pageHeader = manager.PageHeaderContainer;
                var reportHeader = manager.ReportHeaderContainer;
                container = pageHeader.Height < reportHeader.Height
                    ? new BoxModel(pageHeader.X, pageHeader.Y, pageHeader.Width, pageHeader.Height)
                    : new BoxModel(reportHeader.X, reportHeader.Y, reportHeader.Width, reportHeader.Height);

            }
            else
            {
                var pageFooter = manager.PageFooterContainer;
                var reportFooter = manager.ReportFooterContainer;
                container = pageFooter.Height < reportFooter.Height
                    ? new BoxModel(pageFooter.X, pageFooter.Y, pageFooter.Width, pageFooter.Height)
                    : new BoxModel(reportFooter.X, reportFooter.Y, reportFooter.Width, reportFooter.Height);
            }

            var longestText = $"Page {pdf.PageCount + 1} of {pdf.PageCount}";
            using var graph = XGraphics.FromPdfPage(pdf.Pages[^1]);
            var textSize = graph.MeasureString(longestText, Font);

            if (!LayoutHelper.TryCreateMarginBox(container, textSize, this, out var marginBox, HorizontalAlignment))
                return false;
            MarginBox = marginBox;

            if (!LayoutHelper.TryCreatePaddingBox(container, textSize, this, out var paddingBox, HorizontalAlignment))
                return false;
            PaddingBox = paddingBox;

            if (!LayoutHelper.TryCreateContentBox(container, textSize, this, out var contentBox, HorizontalAlignment))
                return false;
            ContentBox = contentBox;

            return true;
        }

        #endregion
    }

    public enum PageNumberPosition
    {
        Header,
        Footer
    }
}