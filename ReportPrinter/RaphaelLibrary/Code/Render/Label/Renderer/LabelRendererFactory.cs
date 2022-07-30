using System;
using RaphaelLibrary.Code.Render.Label.Helper;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelRendererFactory
    {
        public static LabelRendererBase CreateLabelRenderer(string name, int lineIndex)
        {
            var procName = $"LabelRendererFactory.{nameof(CreateLabelRenderer)}";

            if (name == LabelElementHelper.S_SQL)
                return new LabelSqlRenderer(lineIndex);
            else if (name == LabelElementHelper.S_TIMESTAMP)
                return new LabelTimestampRenderer(lineIndex);
            else
            {
                var error = $"Invalid name: {name} for label renderer";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}