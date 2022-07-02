using System.Collections.Generic;
using System.Linq;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Structure
{
    public abstract class PdfStructureBase : IXmlReader
    {
        protected double Height;
        protected int Rows;
        protected int Columns;

        private readonly HashSet<string> _rendererNames;
        private List<PdfRendererBase> _pdfRendererList;

        protected PdfStructureBase(HashSet<string> rendererNames)
        {
            _rendererNames = rendererNames;
            _pdfRendererList = new List<PdfRendererBase>();
        }

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var heightStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_HEIGHT);
            if (!double.TryParse(heightStr?.Substring(0, heightStr.Length - 1), out var height))
            {
                height = this.GetType().Name == PdfStructure.PdfPageBody.ToString() ? 8 : 1;
                Logger.LogDefaultValue(XmlElementHelper.S_HEIGHT, height, procName);
            }
            Height = height;

            var rowsStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ROWS);
            if (!int.TryParse(rowsStr, out var rows))
            {
                rows = 1;
                Logger.LogDefaultValue(XmlElementHelper.S_ROWS, rows, procName);
            }
            Rows = rows;

            var columnsStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_COLUMNS);
            if (!int.TryParse(columnsStr, out var columns))
            {
                columns = 1;
                Logger.LogDefaultValue(XmlElementHelper.S_COLUMNS, columns, procName);
            }
            Columns = columns;

            foreach (var name in _rendererNames)
            {
                var rendererNodes = node.SelectNodes(name);
                foreach (XmlNode rendererNode in rendererNodes)
                {
                    var pdfRenderer = PdfRendererFactory.CreatePdfRenderer(name);
                    if (!pdfRenderer.ReadXml(rendererNode))
                    {
                        return false;
                    }
                    
                    _pdfRendererList.Add(pdfRenderer);
                }
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