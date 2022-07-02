using System.Collections.Generic;
using System.Xml;
using RaphaelLibrary.Code.Common;
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
        protected List<PdfRendererBase> PdfRendererList;

        public virtual bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var heightStr = node.Attributes?[XmlElementName.S_HEIGHT]?.Value;
            if (!double.TryParse(heightStr?.Substring(0, heightStr.Length - 1), out var height))
            {
                height = this.GetType().Name == PdfStructure.PdfPageBody.ToString() ? 8 : 1;
                Logger.LogDefaultValue(XmlElementName.S_HEIGHT, height, procName);
            }
            Height = height;

            var rowsStr = node.Attributes?[XmlElementName.S_ROWS]?.Value;
            if (!int.TryParse(rowsStr, out var rows))
            {
                rows = 1;
                Logger.LogDefaultValue(XmlElementName.S_ROWS, rows, procName);
            }
            Rows = rows;

            var columnsStr = node.Attributes?[XmlElementName.S_COLUMNS]?.Value;
            if (!int.TryParse(columnsStr, out var columns))
            {
                columns = 1;
                Logger.LogDefaultValue(XmlElementName.S_COLUMNS, columns, procName);
            }
            Columns = columns;
            
            return true;
        }

        public void RenderPdfStructure(PdfDocumentManager manager)
        {
            PdfRendererList.ForEach(x => x.RenderPdf(manager));
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