using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelTemplateManager : TemplateManagerBase, IXmlReader
    {
        private static readonly object _lock = new object();
        
        private static LabelTemplateManager _instance;
        public static LabelTemplateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LabelTemplateManager();
                        }
                    }
                }

                return _instance;
            }
        }
        
        private LabelTemplateManager() { }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var labelTemplates = node.SelectNodes(XmlElementHelper.S_LABEL_TEMPLATE);
            if (labelTemplates == null || labelTemplates.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_LABEL_TEMPLATE, node, procName);
                return false;
            }

            foreach (XmlNode labelTemplateNode in labelTemplates)
            {
                var path = labelTemplateNode.InnerText;

                if (!File.Exists(path))
                {
                    Logger.Warn($"Label template: {path} does not exist", procName);
                    continue;
                }

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                var labelTemplate = new LabelTemplate();
                if (!labelTemplate.ReadXml(xmlDoc.DocumentElement))
                {
                    return false;
                }

                if (ReportTemplateList.ContainsKey(labelTemplate.Id))
                {
                    Logger.Error($"Duplicate label template id: {labelTemplate.Id} detected", procName);
                    return false;
                }

                ReportTemplateList.Add(labelTemplate.Id, labelTemplate);
            }

            if (ReportTemplateList.Count == 0)
            {
                Logger.Error($"There is no valid label template in the config", procName);
                return false;
            }

            Logger.Info($"Success to initialize label template manager with {ReportTemplateList.Count} pdf template(s)", procName);
            return true;
        }

        public void Reset()
        {
            _instance = new LabelTemplateManager();
        }
    }
}