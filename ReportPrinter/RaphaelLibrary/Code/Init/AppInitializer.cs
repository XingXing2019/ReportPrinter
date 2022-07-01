using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Init.PDF;
using RaphaelLibrary.Code.Init.SQL;
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

            return true;
        }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var sqlTemplateList = node.SelectSingleNode(XmlElementName.S_SQL_TEMPLATE_LIST);
            if (sqlTemplateList == null)
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.S_SQL_TEMPLATE_LIST, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }

            if (!SqlTemplateManager.Instance.ReadXml(sqlTemplateList))
            {
                return false;
            }

            var pdfTemplateList = node.SelectSingleNode(XmlElementName.S_PDF_TEMPLATE_LIST);
            if (pdfTemplateList == null)
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.S_PDF_TEMPLATE_LIST, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }

            if (!PdfTemplateManager.Instance.ReadXml(pdfTemplateList))
            {
                return false;
            }

            return true;
        }
    }
}