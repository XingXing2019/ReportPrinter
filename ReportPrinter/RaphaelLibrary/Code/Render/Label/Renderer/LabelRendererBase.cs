using System;
using System.Collections.Generic;
using System.Linq;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public abstract class LabelRendererBase
    {
        protected int LineIndex;
        protected List<PlaceHolderBase> PlaceHolders;

        protected LabelRendererBase(int lineIndex)
        {
            LineIndex = lineIndex;
            PlaceHolders = new List<PlaceHolderBase>();
        }

        public bool ReadLine(string line, LabelDeserializeHelper deserializer, string rendererName)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadLine)}";
            if (!deserializer.TryGetPlaceHolders(line, rendererName, LabelElementHelper.S_END, out var placeholders))
                return false;

            if (!TryCreatePlaceHolders(deserializer, placeholders))
                return false;

            Logger.Info($"Success to read {this.GetType().Name}", procName);
            return true;
        }

        protected abstract bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders);

        public LabelRendererBase Clone()
        {
            var cloned = this.MemberwiseClone() as LabelRendererBase;
            cloned.PlaceHolders = this.PlaceHolders.Select(x => x.Clone()).ToList();
            return cloned;
        }

        public virtual bool TryRenderLabel(LabelManager manager)
        {
            var renderName = this.GetType().Name;
            var procName = $"{renderName}.{nameof(TryRenderLabel)}";

            try
            {
                if (PlaceHolders.Any(x => !x.TryReplacePlaceHolder(manager, LineIndex)))
                    return false;

                Logger.Info($"Success to render label: {renderName} for message: {manager.MessageId}", procName);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during rendering label: {renderName} for message: {manager.MessageId}. Ex: {ex.Message}", procName);
                return false;
            }
        }
    }
}