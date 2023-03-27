using System.Collections.Generic;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelReferenceRenderer : LabelRendererBase
    {
        public LabelReferenceRenderer(int lineIndex) : base(lineIndex) { }

        protected override bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreatePlaceHolders)}";

            foreach (var placeholder in placeholders)
            {
                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_STRUCTURE_ID, out var structureId) || string.IsNullOrEmpty(structureId))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_STRUCTURE_ID, placeholder, procName);
                    return false;
                }

                if (!LabelStructureManager.Instance.TryGetLabelStructure(structureId, out var labelStructure))
                    return false;

                var referencePlaceholder = new ReferencePlaceHolder(placeholder, labelStructure);
                PlaceHolders.Add(referencePlaceholder);
            }

            return true;
        }
    }
}