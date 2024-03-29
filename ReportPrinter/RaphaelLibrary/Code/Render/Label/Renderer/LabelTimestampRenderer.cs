﻿using System.Collections.Generic;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelTimestampRenderer : LabelRendererBase
    {
        public LabelTimestampRenderer(int lineIndex) : base(lineIndex) { }
        
        protected override bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreatePlaceHolders)}";

            foreach (var placeholder in placeholders)
            {
                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_MASK, out var mask))
                {
                    mask = "dd/MM/yyyy";
                    Logger.LogDefaultValue(placeholder, LabelElementHelper.S_MASK, mask, procName);
                }

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_IS_UTC, out var isUtcStr) || !bool.TryParse(isUtcStr, out var isUtc))
                {
                    isUtc = false;
                    Logger.LogDefaultValue(placeholder, LabelElementHelper.S_IS_UTC, isUtcStr, procName);
                }

                var timestampPlaceHolder = new TimestampPlaceHolder(placeholder, isUtc, mask);
                PlaceHolders.Add(timestampPlaceHolder);
            }

            return true;
        }
    }
}