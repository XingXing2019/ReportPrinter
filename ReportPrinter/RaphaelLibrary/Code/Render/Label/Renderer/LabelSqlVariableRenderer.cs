using System.Collections.Generic;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelSqlVariableRenderer : LabelRendererBase
    {
        public LabelSqlVariableRenderer(int lineIndex) : base(lineIndex) { }

        protected override bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryCreatePlaceHolders)}";

            foreach (var placeholder in placeholders)
            {
                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_NAME, out var name))
                {
                    Logger.LogMissingFieldLog(LabelElementHelper.S_NAME, placeholder, procName);
                    return false;
                }

                var sqlVariablePlaceHolder = new SqlVariablePlaceHolder(placeholder, name);
                PlaceHolders.Add(sqlVariablePlaceHolder);
            }

            return true;
        }
    }
}