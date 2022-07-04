using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.PDF;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Config.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init
{
    public class AppInitializer : IXmlReader
    {
        public bool Execute()
        {
            var procName = $"{this.GetType().Name}.{nameof(Execute)}";

            var path = new ConfigPath().GetAppConfigPath();
            if (!File.Exists(path))
            {
                Logger.Error($"Configure file: {path} does not exist", procName);
                return false;
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            if (!ReadXml(xmlDoc.DocumentElement))
            {
                Logger.Error($"Unable to read xml file: {path}", procName);
                return false;
            }

            Logger.Info($"Success to initialize application", procName);
            return true;
        }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            if (node == null)
            {
                Logger.LogMissingXmlLog("RaphaelConfig", node, procName);
                return false;
            }

            var sqlTemplateList = node.SelectSingleNode(XmlElementHelper.S_SQL_TEMPLATE_LIST);
            if (sqlTemplateList == null)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SQL_TEMPLATE_LIST, node, procName);
                return false;
            }

            if (!SqlTemplateManager.Instance.ReadXml(sqlTemplateList))
            {
                return false;
            }

            var pdfTemplateList = node.SelectSingleNode(XmlElementHelper.S_PDF_TEMPLATE_LIST);
            if (pdfTemplateList == null)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_PDF_TEMPLATE_LIST, node, procName);
                return false;
            }

            if (!PdfTemplateManager.Instance.ReadXml(pdfTemplateList))
            {
                return false;
            }

            Logger.Info($"Success to read config of RaphaelLibrary", procName);
            return true;
        }
    }
}