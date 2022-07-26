using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Renderer;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelStructure : IStructure
    {
        private readonly LabelDeserializeHelper _deserializer;
        private readonly Dictionary<string, string> _labelRenderer;
        private string[] _lines;


        public string Id { get; }

        public LabelStructure(string id, LabelDeserializeHelper deserializer, Dictionary<string, string> labelRenderer)
        {
            Id = id;
            _deserializer = deserializer;
            _labelRenderer = labelRenderer
                .Where(x => x.Key != LabelElementHelper.S_START && x.Key != LabelElementHelper.S_END)
                .ToDictionary(x => x.Key, x => x.Value);
        }
        
        public IStructure Clone()
        {
            throw new System.NotImplementedException();
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

        public bool TryCreateLabelStructure()
        {
            throw new System.NotImplementedException();
        }


        #region Helper

        private bool ReadLine(string line, int lineIndex)
        {
            foreach (var rendererName in _labelRenderer.Keys)
            {
                if (line.Contains(_labelRenderer[rendererName]))
                {
                    var labelRenderer = LabelRendererFactory.CreateLabelRenderer(rendererName, lineIndex);
                    if (!labelRenderer.ReadLine(line, _deserializer))
                        return false;
                }
            }

            return true;
        }

        #endregion
    }
}