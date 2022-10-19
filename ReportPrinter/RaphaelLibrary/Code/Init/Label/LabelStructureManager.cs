using System.Collections.Generic;
using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.PDF.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelStructureManager : IXmlReader
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<string, IStructure> _labelStructureList;


        private static LabelStructureManager _instance;
        public static LabelStructureManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LabelStructureManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private LabelStructureManager()
        {
            _labelStructureList = new Dictionary<string, IStructure>();
        }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var labelStructures = node.SelectNodes(XmlElementHelper.S_LABEL_STRUCTURE);
            if (labelStructures == null || labelStructures.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_LABEL_STRUCTURE, node, procName);
                return false;
            }

            foreach (XmlNode labelStructureNode in labelStructures)
            {
                var path = labelStructureNode.InnerText;

                if (!File.Exists(path))
                {
                    Logger.Warn($"Label structure: {path} does not exists", procName);
                    continue;
                }

                var id = XmlElementHelper.GetAttribute(labelStructureNode, XmlElementHelper.S_ID);
                if (string.IsNullOrEmpty(id))
                {
                    Logger.LogMissingXmlLog(XmlElementHelper.S_ID, labelStructureNode, procName);
                    return false;
                }

                var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);
                var labelStructure = new LabelStructure(id, deserializer, LabelElementHelper.LABEL_RENDERER);
                if (!labelStructure.ReadFile(path))
                {
                    return false;
                }

                if (_labelStructureList.ContainsKey(labelStructure.Id))
                {
                    Logger.Error($"Duplicate label structure: {labelStructure.Id} detected", procName);
                    return false;
                }

                _labelStructureList.Add(labelStructure.Id, labelStructure);
            }

            if (_labelStructureList.Count == 0)
            {
                Logger.Error($"These is no valid label template in the config", procName);
                return false;
            }

            Logger.Info($"Success to initialize label structure manager with {_labelStructureList.Count} label structure(s)", procName);
            return true;
        }

        public bool TryGetLabelStructure(string labelStructureId, out IStructure labelStructure)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetLabelStructure)}";
            labelStructure = null;

            if (!_labelStructureList.ContainsKey(labelStructureId))
            {
                Logger.Error($"Label structure id: {labelStructureId} does not exist in label structure manager", procName);
                return false;
            }

            labelStructure = _labelStructureList[labelStructureId].Clone();

            Logger.Debug($"Return a deep clone of label structure: {labelStructureId}", procName);
            return true;
        }

        public void Reset()
        {
            _instance = new LabelStructureManager();
        }
    }
}