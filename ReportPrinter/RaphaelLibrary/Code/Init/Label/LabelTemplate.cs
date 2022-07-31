using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Print;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelTemplate : TemplateBase, IXmlReader
    {
        private string _savePath;
        private string _fileNameSuffix;
        private string _fileName;
        private int _timeout;
        private List<IStructure> _labelStructures;

        public string Id { get; private set; }

        public LabelTemplate()
        {
            _labelStructures = new List<IStructure>();
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

            var labelHeaderId = node.SelectSingleNode(XmlElementHelper.S_LABEL_HEADER)?.InnerText;
            if (!string.IsNullOrEmpty(labelHeaderId))
            {
                if (!LabelStructureManager.Instance.TryGetLabelStructure(labelHeaderId, out var labelHeader))
                    return false;
                _labelStructures.Add(labelHeader);
            }

            var labelBodyId = node.SelectSingleNode(XmlElementHelper.S_LABEL_BODY)?.InnerText;
            if (string.IsNullOrEmpty(labelBodyId))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_LABEL_BODY, node, procName);
                return false;
            }

            if (!LabelStructureManager.Instance.TryGetLabelStructure(labelBodyId, out var labelBody))
                return false;
            _labelStructures.Add(labelBody);

            var labelFooterId = node.SelectSingleNode(XmlElementHelper.S_LABEL_FOOTER)?.InnerText;
            if (!string.IsNullOrEmpty(labelFooterId))
            {
                if (!LabelStructureManager.Instance.TryGetLabelStructure(labelFooterId, out var labelFooter))
                    return false;
                _labelStructures.Add(labelFooter);
            }

            Logger.Info($"Success to read label template: {Id}, file name suffix: {_fileNameSuffix}, save path: {_savePath}", procName);
            return true;
        }

        public override TemplateBase Clone()
        {
            var cloned = this.MemberwiseClone() as LabelTemplate;
            cloned._labelStructures = this._labelStructures.Select(x => x.Clone()).ToList();
            return cloned;
        }

        public override bool TryCreateReport(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreateReport)}";
            
            try
            {
                StoreSqlVariables(message);

                var labelLines = new StringBuilder();
                foreach (var labelStructure in _labelStructures)
                {
                    if (labelStructure == null) continue;
                    if (!labelStructure.TryCreateLabelStructure(message.MessageId, out var lines))
                        return false;
                    labelLines.Append(lines);
                    labelLines.AppendLine();
                }

                var fileName = $"{_fileName}_{message.MessageId}";
                var filePath = $"{_savePath}{fileName}.zpl";
                FileHelper.CreateFile(filePath, labelLines.ToString());

                Logger.Info($"Success to create label for massage: {message.MessageId}", procName);

                if (!string.IsNullOrEmpty(message.PrinterId))
                {
                    PrintReport(message, fileName, filePath, _timeout);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during creating label report for message: {message.MessageId}. Ex: {ex.Message}", procName);
                return false;
            }
            finally
            {
                SqlVariableManager.Instance.RemoveSqlVariables(message.MessageId);
                SqlResultCacheManager.Instance.RemoveSqlResult(message.MessageId);
            }
        }
    }
}