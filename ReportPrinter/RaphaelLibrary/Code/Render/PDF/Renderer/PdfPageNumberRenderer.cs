using System;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Renderer
{
    public class PdfPageNumberRenderer : PdfRendererBase
    {
        private int _startPage;
        private int _endPage;
        private Location _pageNumberLocation;

        public PdfPageNumberRenderer(PdfStructure location) : base(location) { }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (!base.ReadXml(node))
            {
                return false;
            }

            var pageNumberLocationStr = node.SelectSingleNode(XmlElementHelper.S_LOCATION)?.InnerText;
            if (!Enum.TryParse(pageNumberLocationStr, out Location pageNumberLocation))
            {
                pageNumberLocation = Location.Footer;
                Logger.LogDefaultValue(node, XmlElementHelper.S_LOCATION, pageNumberLocation, procName);
            }
            _pageNumberLocation = pageNumberLocation;

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
            
            Logger.Info($"Success to read {this.GetType().Name}, start page: {_startPage}, end page: {_endPage}", procName);
            return true;
        }

        protected override bool TryPerformRender(PdfDocumentManager manager, string procName)
        {
            var pdf = manager.Pdf;
            var longestText = $"Page {pdf.PageCount + 1} of {pdf.PageCount}";
            using (var graph = XGraphics.FromPdfPage(pdf.Pages[^1]))
            {
                var textSize = graph.MeasureString(longestText, Font);
                if (!TryCalcRendererPosition(manager, textSize, _pageNumberLocation))
                    return false;
            }
            
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

       

        #endregion
    }
}