using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Print;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init.PDF
{
    public class PdfTemplate : ITemplate
    {
        public string Id { get; private set; }

        private Dictionary<PdfStructure, PdfStructureBase> _pdfStructureList;
        private Dictionary<PdfStructure, ContainerModel> _pdfStructureSizeList;

        private XSize _pageSize;
        private string _fileName;
        private string _fileNameSuffix;
        private string _savePath;
        private int _timeout;

        private readonly HashSet<string> _rendererInHeaderFooter;
        private readonly HashSet<string> _rendererInBody;

        public PdfTemplate()
        {
            _pdfStructureList = new Dictionary<PdfStructure, PdfStructureBase>();
            _pdfStructureSizeList = new Dictionary<PdfStructure, ContainerModel>();

            _rendererInHeaderFooter = new HashSet<string>
            {
                XmlElementHelper.S_TEXT,
                XmlElementHelper.S_BARCODE,
                XmlElementHelper.S_IMAGE,
                XmlElementHelper.S_ANNOTATION,
            };

            _rendererInBody = new HashSet<string>
            {
                XmlElementHelper.S_TABLE,
                XmlElementHelper.S_WATER_MARK,
                XmlElementHelper.S_PAGE_NUMBER,
                XmlElementHelper.S_REPRINT_MARK
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
            if (!FileHelper.DirectoryExists(_savePath))
            {
                FileHelper.CreateDirectory(_savePath);
            }

            var fileNameSuffix = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_FILE_NAME_SUFFIX);
            if (string.IsNullOrEmpty(fileNameSuffix))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_FILE_NAME_SUFFIX, node, procName);
                return false;
            }
            _fileNameSuffix = fileNameSuffix;

            var timeoutStr = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_TIMEOUT);
            if (!int.TryParse(timeoutStr, out var timeout))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_TIMEOUT, node, procName);
                return false;
            }
            _timeout = timeout;

            var reportHeader = new PdfReportHeader(_rendererInHeaderFooter);
            if (!reportHeader.ReadXml(node.SelectSingleNode(XmlElementHelper.S_REPORT_HEADER)))
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
            if (!reportFooter.ReadXml(node.SelectSingleNode(XmlElementHelper.S_REPORT_FOOTER)))
            {
                return false;
            }
            _pdfStructureList.Add(PdfStructure.PdfReportFooter, reportFooter);

            foreach (var structure in _pdfStructureList.Keys)
            {
                _pdfStructureList[structure].Height = LayoutHelper.CalcPdfStructureHeight(_pageSize, structure, _pdfStructureList);
            }
            
            foreach (var structure in _pdfStructureList.Keys)
            {
                if (structure == PdfStructure.PdfPageBody)
                    continue;

                var container = LayoutHelper.CreateContainer(_pageSize, structure, _pdfStructureList);
                if (!_pdfStructureList[structure].TryCalcRendererPosition(container))
                    return false;

                _pdfStructureSizeList[structure] = new ContainerModel
                {
                    LeftBoundary = _pdfStructureList[structure].Margin.Left + _pdfStructureList[structure].Padding.Left,
                    RightBoundary = _pageSize.Width - _pdfStructureList[structure].Margin.Right - _pdfStructureList[structure].Padding.Right,
                    FirstPageTopBoundary = container.Y,
                    NonFirstPageTopBoundary = container.Y,
                    LastPageBottomBoundary = container.Y + container.Height,
                    NonLastPageBottomBoundary = container.Y + container.Height
                };
            }

            _pdfStructureSizeList[PdfStructure.PdfPageBody] = new ContainerModel
            {
                LeftBoundary = pageBody.Margin.Left + pageBody.Padding.Left,
                RightBoundary = _pageSize.Width - pageBody.Margin.Right - pageBody.Padding.Right,
                FirstPageTopBoundary = reportHeader.Height + pageBody.Margin.Top + pageBody.Padding.Top,
                NonFirstPageTopBoundary = pageHeader.Height + pageBody.Margin.Top + pageBody.Padding.Top,
                LastPageBottomBoundary = _pageSize.Height - reportFooter.Height - pageBody.Margin.Bottom - pageBody.Padding.Bottom,
                NonLastPageBottomBoundary = _pageSize.Height - pageFooter.Height - pageBody.Margin.Bottom - pageBody.Padding.Bottom
            };


            Logger.Info($"Success to read pdf template: {Id}, page size: {_pageSize.Width} : {_pageSize.Height}, Orientation: {orientation}, " +
                        $"file name suffix: {_fileNameSuffix}, save path: {_savePath}", procName);
            return true;
        }

        public ITemplate Clone()
        {
            var cloned = this.MemberwiseClone() as PdfTemplate;

            cloned._pdfStructureList = new Dictionary<PdfStructure, PdfStructureBase>();
            foreach (var structure in this._pdfStructureList.Keys)
            {
                cloned._pdfStructureList.Add(structure, this._pdfStructureList[structure].Clone());
            }

            cloned._pdfStructureSizeList = new Dictionary<PdfStructure, ContainerModel>();
            foreach (var structure in this._pdfStructureSizeList.Keys)
            {
                cloned._pdfStructureSizeList.Add(structure, this._pdfStructureSizeList[structure].Clone());
            }

            return cloned;
        }

        public bool TryCreateReport(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreateReport)}";

            var sqlVariables = message.SqlVariables.ToDictionary(x => x.Name, x => new SqlVariable { Name = x.Name, Value = x.Value });
            var hasReprintMark = message.HasReprintFlag ?? false;
            var messageId = message.MessageId;
            
            try
            {
                SqlVariableManager.Instance.StoreSqlVariables(messageId, sqlVariables);

                var pdf = new PdfDocument();
                var manager = new PdfDocumentManager(messageId, pdf, hasReprintMark, _pageSize, _pdfStructureSizeList);

                var pageBody = _pdfStructureList[PdfStructure.PdfPageBody];
                if (!pageBody.TryRenderPdfStructure(manager))
                    return false;

                var headerFooter = _pdfStructureList.Where(x => x.Key != PdfStructure.PdfPageBody).Select(x => x.Value);
                if (headerFooter.Any(x => !x.TryRenderPdfStructure(manager)))
                    return false;

                var fileName = $"{_fileName}_{manager.MessageId}";
                var filePath = $"{_savePath}{fileName}.pdf";
                manager.Pdf.Save(filePath);

                Logger.Info($"Success to create pdf: {Id} for message: {manager.MessageId}", procName);

                if (!string.IsNullOrEmpty(message.PrinterId))
                {
                    var printer = PrinterFactory.CreatePrinter(message.ReportType);
                    if (!printer.PrintReport(fileName, filePath, message.PrinterId, message.NumberOfCopy, _timeout))
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during creating pdf report for message: {messageId}. Ex: {ex.Message}", procName);
                return false;
            }
            finally
            {
                SqlVariableManager.Instance.RemoveSqlVariables(messageId);
                SqlResultCacheManager.Instance.RemoveSqlResult(messageId);
                ImageCacheManager.Instance.RemoveImage(messageId);
            }
        }
    }

    public enum Orientation
    {
        Portrait,
        Landscape
    }
}