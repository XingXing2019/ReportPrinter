using System.Collections.Generic;
using System.Linq;
using System.Xml;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public abstract class PdfStructureBase : IXmlReader
    {
        public double Height { get; set; }

        protected int Rows;
        protected int Columns;

        private readonly PdfStructure _position;
        private readonly HashSet<string> _rendererNames;
        private List<PdfRendererBase> _pdfRendererList;
        private MarginPaddingModel _margin;
        private MarginPaddingModel _padding;

        protected PdfStructureBase(PdfStructure position, HashSet<string> rendererNames)
        {
            _position = position;
            _rendererNames = rendererNames;
            _pdfRendererList = new List<PdfRendererBase>();
        }

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (node == null)
            {
                Logger.LogMissingXmlLog(_position.ToString(), node, procName);
                return false;
            }

            var heightStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HEIGHT);
            if (!double.TryParse(heightStr?.Substring(0, heightStr.Length - 1), out var height))
            {
                height = _position == PdfStructure.PdfPageBody ? 8 : 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_HEIGHT, height, procName);
            }
            Height = height;

            var rowsStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ROWS);
            if (!int.TryParse(rowsStr, out var rows))
            {
                rows = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_ROWS, rows, procName);
            }
            Rows = rows;

            var columnsStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_COLUMNS);
            if (!int.TryParse(columnsStr, out var columns))
            {
                columns = 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_COLUMNS, columns, procName);
            }
            Columns = columns;

            var marginStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_MARGIN);
            if (!LayoutHelper.TryCreateMarginPadding(marginStr, out var margin))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_MARGIN, "0", procName);
            }
            _margin = margin;

            var paddingStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PADDING);
            if (!LayoutHelper.TryCreateMarginPadding(paddingStr, out var padding))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_PADDING, padding, procName);
            }
            _padding = padding;

            foreach (var name in _rendererNames)
            {
                var rendererNodes = node.SelectNodes(name);
                foreach (XmlNode rendererNode in rendererNodes)
                {
                    var pdfRenderer = PdfRendererFactory.CreatePdfRenderer(name, _position);
                    if (!pdfRenderer.ReadXml(rendererNode))
                    {
                        return false;
                    }
                    
                    _pdfRendererList.Add(pdfRenderer);
                }
            }

            Logger.Info($"Success to read {_position} with {_pdfRendererList.Count} pdf renderer, height: {Height}, rows: {Rows}, columns: {Columns}", procName);
            return true;
        }

        public bool TryCalcRendererPosition(XSize pageSize, double totalHeight)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCalcRendererPosition)}";

            var height = Height / totalHeight * pageSize.Height - _margin.Top - _margin.Bottom - _padding.Top - _padding.Bottom;
            var width = pageSize.Width - _margin.Left - _margin.Right - _padding.Left - _padding.Right;

            if (height <= 0)
            {
                Logger.Error($"{_position}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (width <= 0)
            {
                Logger.Error($"{_position}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            foreach (var renderer in _pdfRendererList)
            {
                var layoutParam = renderer.GetLayoutParameter();

                if (!LayoutHelper.TryCalcMarginBoxParameter(out var marginBox))
                    return false;
                renderer.SetMarginBox(marginBox);

                if (!LayoutHelper.TryCalcPaddingBoxParameter(out var paddingBox))
                    return false;
                renderer.SetPaddingBox(paddingBox);

                if (!LayoutHelper.TryCalcContentBoxParameter(out var contentBox))
                    return false;
                renderer.SetContentBox(contentBox);
            }

            return true;
        }

        public PdfStructureBase Clone()
        {
            var cloned = this.MemberwiseClone() as PdfStructureBase;
            cloned._pdfRendererList = this._pdfRendererList.Select(x => x.Clone()).ToList();
            return cloned;
        }

        public void RenderPdfStructure(PdfDocumentManager manager)
        {
            _pdfRendererList.ForEach(x => x.RenderPdf(manager));
        }
    }

    public enum PdfStructure
    {
        PdfReportHeader,
        PdfPageHeader,
        PdfPageBody,
        PdfPageFooter,
        PdfReportFooter
    }
}