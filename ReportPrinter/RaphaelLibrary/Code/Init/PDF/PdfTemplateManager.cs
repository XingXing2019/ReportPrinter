using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.PDF
{
    public class PdfTemplateManager : TemplateManagerBase, IXmlReader
    {
        private static readonly object _lock = new object();

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

        private PdfTemplateManager() { }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var pdfTemplates = node.SelectNodes(XmlElementHelper.S_PDF_TEMPLATE);
            if (pdfTemplates == null || pdfTemplates.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_PDF_TEMPLATE, node, procName);
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

                if (ReportTemplateList.ContainsKey(pdfTemplate.Id))
                {
                    Logger.Error($"Duplicate pdf template id: {pdfTemplate.Id} detected", procName);
                    return false;
                }

                ReportTemplateList.Add(pdfTemplate.Id, pdfTemplate);
            }

            if (ReportTemplateList.Count == 0)
            {
                Logger.Error($"There is no valid pdf template in the config", procName);
                return false;
            }

            Logger.Info($"Success to initialize pdf template manager with {ReportTemplateList.Count} pdf template(s)", procName);
            return true;
        }

        public void Reset()
        {
            _instance = new PdfTemplateManager();
        }
    }
}