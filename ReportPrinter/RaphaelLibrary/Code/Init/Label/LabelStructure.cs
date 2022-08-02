using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.Renderer;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelStructure : IStructure
    {
        private readonly LabelDeserializeHelper _deserializer;
        private readonly Dictionary<string, string> _labelRendererSigns;

        private List<LabelRendererBase> _labelRenderer;
        private string[] _lines;
        
        public string Id { get; }

        public LabelStructure(string id, LabelDeserializeHelper deserializer, Dictionary<string, string> labelRenderer)
        {
            Id = id;
            _deserializer = deserializer;
            _labelRendererSigns = labelRenderer
                .Where(x => x.Key != LabelElementHelper.S_START && x.Key != LabelElementHelper.S_END)
                .ToDictionary(x => x.Key, x => x.Value);
            _labelRenderer = new List<LabelRendererBase>();
        }
        
        public IStructure Clone()
        {
            var cloned = this.MemberwiseClone() as LabelStructure;
            cloned._labelRenderer = this._labelRenderer.Select(x => x.Clone()).ToList();
            cloned._lines = new string[this._lines.Length];
            for (int i = 0; i < this._lines.Length; i++)
            {
                cloned._lines[i] = this._lines[i];
            }
            return cloned;
        }

        public bool ReadFile(string filePath)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadFile)}";
            var input = File.ReadAllLines(filePath);

            try
            {
                if (!_deserializer.TryGetLines(input, out var lines))
                    return false;

                if (lines.Where((line, lineIndex) => !ReadLine(line, lineIndex)).Any())
                    return false;

                _lines = lines;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during read file: {filePath}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        public bool TryCreateLabelStructure(Guid messageId, out StringBuilder lines)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreateLabelStructure)}";

            lines = new StringBuilder();
            var manager = new LabelManager(_lines, messageId);
            if (_labelRenderer.Any(x => !x.TryRenderLabel(manager)))
                return false;
            
            foreach (var line in _lines)
            {
                lines.AppendLine(line);
            }

            Logger.Info($"Success to create label structure for message: {messageId}", procName);
            return true;
        }


        #region Helper

        private bool ReadLine(string line, int lineIndex)
        {
            foreach (var rendererName in _labelRendererSigns.Keys)
            {
                if (_deserializer.ContainsPlaceholder(line, _labelRendererSigns[rendererName]))
                {
                    var labelRenderer = LabelRendererFactory.CreateLabelRenderer(rendererName, lineIndex);
                    if (!labelRenderer.ReadLine(line, _deserializer, rendererName))
                        return false;
                    _labelRenderer.Add(labelRenderer);
                }
            }

            return true;
        }
        
        #endregion
    }
}