using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
        public double HeightRatio { get; private set; }
        public MarginPaddingModel Margin { get; private set; }
        public MarginPaddingModel Padding { get; private set; }

        protected int Rows;
        protected int Columns;
        protected readonly PdfStructure Location;
        protected List<PdfRendererBase> PdfRendererList;

        private readonly HashSet<string> _rendererNames;


        protected PdfStructureBase(PdfStructure location, HashSet<string> rendererNames)
        {
            Location = location;
            _rendererNames = rendererNames;
            PdfRendererList = new List<PdfRendererBase>();
        }

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (node == null)
            {
                Logger.LogMissingXmlLog(Location.ToString(), node, procName);
                return false;
            }

            var heightRatioStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HEIGHT);
            if (!double.TryParse(heightRatioStr?.Substring(0, heightRatioStr.Length - 1), out var heightRatio))
            {
                heightRatio = Location == PdfStructure.PdfPageBody ? 8 : 1;
                Logger.LogDefaultValue(node, XmlElementHelper.S_HEIGHT, heightRatio, procName);
            }
            HeightRatio = heightRatio;

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
            Margin = margin;

            var paddingStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PADDING);
            if (!LayoutHelper.TryCreateMarginPadding(paddingStr, out var padding))
            {
                Logger.LogDefaultValue(node, XmlElementHelper.S_PADDING, padding, procName);
            }
            Padding = padding;

            foreach (var name in _rendererNames)
            {
                var rendererNodes = node.SelectNodes(name);
                foreach (XmlNode rendererNode in rendererNodes)
                {
                    var pdfRenderer = PdfRendererFactory.CreatePdfRenderer(name, Location);
                    if (!pdfRenderer.ReadXml(rendererNode))
                    {
                        return false;
                    }
                    
                    PdfRendererList.Add(pdfRenderer);
                }
            }

            Logger.Info($"Success to read {Location} with {PdfRendererList.Count} pdf renderer, height ratio: {HeightRatio}*, rows: {Rows}, columns: {Columns}", procName);
            return true;
        }

        public bool TryCalcRendererPosition(BoxModel container)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCalcRendererPosition)}";

            var x = container.X;
            var y = container.Y;
            var height = container.Height;
            var width = container.Width;

            if (height <= 0)
            {
                Logger.Error($"{Location}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (width <= 0)
            {
                Logger.Error($"{Location}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            foreach (var renderer in PdfRendererList)
            {
                var layoutParam = renderer.GetLayoutParameter();
                if (layoutParam.Row + layoutParam.RowSpan > Rows)
                {
                    Logger.Error($"Total rows: {Rows} is not enough for render: {renderer.GetType().Name} located at row: {layoutParam.Row} and {layoutParam.RowSpan} row span", procName);
                    return false;
                }

                if (layoutParam.Column + layoutParam.ColumnSpan > Columns)
                {
                    Logger.Error($"Total columns: {Columns} is not enough for render: {renderer.GetType().Name} located at row: {layoutParam.Column} and {layoutParam.ColumnSpan} column span", procName);
                    return false;
                }

                if (!LayoutHelper.TryCreateMarginBox(new BoxModel(x, y, width, height), Rows, Columns, renderer, out var marginBox))
                    return false;
                marginBox = LayoutHelper.AdjustBoxLocation(marginBox, layoutParam);
                renderer.SetMarginBox(marginBox);

                if (!LayoutHelper.TryCreatePaddingBox(new BoxModel(x, y, width, height), Rows, Columns, renderer, out var paddingBox))
                    return false;
                paddingBox = LayoutHelper.AdjustBoxLocation(paddingBox, layoutParam);
                renderer.SetPaddingBox(paddingBox);

                if (!LayoutHelper.TryCreateContentBox(new BoxModel(x, y, width, height), Rows, Columns, renderer, out var contentBox))
                    return false;
                contentBox = LayoutHelper.AdjustBoxLocation(contentBox, layoutParam);
                renderer.SetContentBox(contentBox);
            }

            return true;
        }

        public PdfStructureBase Clone()
        {
            var cloned = this.MemberwiseClone() as PdfStructureBase;
            cloned.PdfRendererList = this.PdfRendererList.Select(x => x.Clone()).ToList();
            return cloned;
        }

        public abstract bool TryRenderPdfStructure(PdfDocumentManager manager);
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