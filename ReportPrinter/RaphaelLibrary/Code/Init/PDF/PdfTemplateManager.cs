using System.Collections.Generic;
using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.PDF
{
    public class PdfTemplateManager : IXmlReader
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<string, PdfTemplate> _pdfTemplateList;

        private static PdfTemplateManager _instance;
        public static PdfTemplateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PdfTemplateManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private PdfTemplateManager()
        {
            _pdfTemplateList = new Dictionary<string, PdfTemplate>();
        }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var pdfTemplates = node.SelectNodes(XmlElementName.S_PDF_TEMPLATE);
            if (pdfTemplates == null || pdfTemplates.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementName.S_PDF_TEMPLATE, node, procName);
                return false;
            }

            foreach (XmlNode pdfTemplateNode in pdfTemplates)
            {
                var path = pdfTemplateNode.InnerText;

                if (!File.Exists(path))
                {
                    Logger.Warn($"Pdf template: {path} does not exist", procName);
                    continue;
                }

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                var pdfTemplate = new PdfTemplate();
                if (!pdfTemplate.ReadXml(xmlDoc.DocumentElement))
                {
                    return false;
                }

                if (_pdfTemplateList.ContainsKey(pdfTemplate.Id))
                {
                    Logger.Error($"Duplicate pdf template id: {pdfTemplate.Id} detected", procName);
                    return false;
                }

                _pdfTemplateList.Add(pdfTemplate.Id, pdfTemplate);
            }

            if (_pdfTemplateList.Count == 0)
            {
                Logger.Error($"There is no valid pdf template in the config", procName);
                return false;
            }

            Logger.Info($"Success to initialize pdf template manager with {_pdfTemplateList.Count} pdf template(s)", procName);
            return true;
        }

        /// <summary>
        /// Get a copy of pdf template
        /// </summary>
        /// <returns></returns>
        public bool TryGetPdfTemplate(string pdfTemplateId, out PdfTemplate pdfTemplate)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetPdfTemplate)}";
            pdfTemplate = null;

            if (!_pdfTemplateList.ContainsKey(pdfTemplateId))
            {
                Logger.Error($"Pdf template id: {pdfTemplateId} does not exist in pdf template manager", procName);
                return false;
            }
            
            pdfTemplate = _pdfTemplateList[pdfTemplateId].Clone();

            Logger.Debug($"Return a deep clone of pdf template: {pdfTemplateId}", procName);
            return true;
        }
    }
}