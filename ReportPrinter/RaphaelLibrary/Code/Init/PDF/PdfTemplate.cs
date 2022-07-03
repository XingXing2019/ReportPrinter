using System;
using System.Collections.Generic;
using System.Xml;
using PdfSharp;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.PDF
{
    public class PdfTemplate : IXmlReader
    {
        public string Id { get; private set; }

        private Dictionary<PdfStructure, PdfStructureBase> _pdfStructureList;
        private XSize _pageSize;
        private string _fileName;
        private string _fileNameSuffix;
        private string _savePath;

        private HashSet<string> _rendererInHeaderFooter;
        private HashSet<string> _rendererInBody;

        public PdfTemplate()
        {
            _pdfStructureList = new Dictionary<PdfStructure, PdfStructureBase>();
            _rendererInHeaderFooter = new HashSet<string>
            {
                XmlElementHelper.S_TEXT
            };

            _rendererInBody = new HashSet<string>
            {

            };
        }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var id = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ID);
            if (string.IsNullOrEmpty(id))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ID, node, procName);
                return false;
            }
            Id = id;
            _fileName = id;

            var pageSizeStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_PAGE_SIZE);
            if (!PageHelper.TryGetPageSize(pageSizeStr, out var pageSize))
            {
                pageSize = PageSizeConverter.ToSize(PageSize.A4);
                Logger.LogDefaultValue(node, XmlElementHelper.S_PAGE_SIZE, PageSize.A4, procName);
            }

            _pageSize = pageSize;

            var orientationStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ORIENTATION);
            if (!Enum.TryParse(orientationStr, out Orientation orientation))
            {
                orientation = Orientation.Portrait;
                Logger.LogDefaultValue(node, XmlElementHelper.S_ORIENTATION, orientation, procName);
            }

            if (orientation == Orientation.Landscape)
            {
                var temp = _pageSize.Height;
                pageSize.Height = _pageSize.Width;
                pageSize.Width = temp;
            }

            var savePath = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_SAVE_PATH);
            if (string.IsNullOrEmpty(savePath))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SAVE_PATH, node, procName);
                return false;
            }
            _savePath = savePath;

            var fileNameSuffix = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FILE_NAME_SUFFIX);
            if (string.IsNullOrEmpty(fileNameSuffix))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_FILE_NAME_SUFFIX, node, procName);
                return false;
            }
            _fileNameSuffix = fileNameSuffix;

            var reportHeader = new PdfReportHeader(_rendererInHeaderFooter);
            if (!reportHeader.ReadXml(node.SelectSingleNode(XmlElementHelper.S_ReportHeader)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfReportHeader, reportHeader);
            
            var pageHeader = new PdfPageHeader(_rendererInHeaderFooter);
            if (!pageHeader.ReadXml(node.SelectSingleNode(XmlElementHelper.S_PAGE_HEADER)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfPageHeader, pageHeader);
            
            var pageBody = new PdfPageBody(_rendererInBody);
            if (!pageBody.ReadXml(node.SelectSingleNode(XmlElementHelper.S_PAGE_BODY)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfPageBody, pageBody);

            var pageFooter = new PdfPageFooter(_rendererInHeaderFooter);
            if (!pageFooter.ReadXml(node.SelectSingleNode(XmlElementHelper.S_PAGE_FOOTER)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfPageFooter, pageFooter);
            
            var reportFooter = new PdfReportFooter(_rendererInHeaderFooter);
            if (!reportFooter.ReadXml(node.SelectSingleNode(XmlElementHelper.S_ReportFooter)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfReportFooter, reportFooter);

            var coverPagesTotalHeight = reportHeader.Height + pageBody.Height + reportFooter.Height;
            var midPagesTotalHeight = pageHeader.Height + pageBody.Height + pageFooter.Height;
            foreach (var structure in _pdfStructureList.Keys)
            {
                if (structure == PdfStructure.PdfPageBody)
                    continue;
                if (structure == PdfStructure.PdfReportHeader || structure == PdfStructure.PdfReportFooter)
                {
                    if (!_pdfStructureList[structure].TryCalcRendererPosition(_pageSize, coverPagesTotalHeight))
                        return false;
                }
                else
                {
                    if (!_pdfStructureList[structure].TryCalcRendererPosition(_pageSize, midPagesTotalHeight))
                        return false;
                }
            }

            Logger.Info($"Success to read pdf template: {Id}, page size: {_pageSize.Width} : {_pageSize.Height}, Orientation: {orientation}, " +
                        $"file name suffix: {_fileNameSuffix}, save path: {_savePath}", procName);
            return true;
        }

        public PdfTemplate Clone()
        {
            var cloned = this.MemberwiseClone() as PdfTemplate;
            cloned._pdfStructureList = new Dictionary<PdfStructure, PdfStructureBase>();
            foreach (var structure in this._pdfStructureList.Keys)
            {
                cloned._pdfStructureList.Add(structure, this._pdfStructureList[structure].Clone());
            }

            return cloned;
        }

        public bool TryCreatePdfReport(PdfDocumentManager manager)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreatePdfReport)}";

            try
            {
                foreach (var pdfStructure in _pdfStructureList.Values)
                {
                    pdfStructure.RenderPdfStructure(manager);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during creating pdf report. Ex: {ex.Message}", procName);
                return false;
            }
        }
    }

    public enum Orientation
    {
        Portrait,
        Landscape
    }
}