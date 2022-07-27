using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelSqlRenderer : LabelRendererBase
    {
        public LabelSqlRenderer(int lineIndex) : base(lineIndex) { }

        public override bool ReadLine(string line, LabelDeserializeHelper deserializer)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadLine)}";
            if (!deserializer.TryGetPlaceHolders(line, LabelElementHelper.S_SQL, LabelElementHelper.S_END, out var placeholders))
                return false;

            foreach (var placeholder in placeholders)
            {
                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_TEMPLATE_ID, out var sqlTemplateId))
                    return false;

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_ID, out var sqlId))
                    return false;

                if (!SqlTemplateManager.Instance.TryGetSql(sqlTemplateId, sqlId, out var sql))
                    return false;

                if (!deserializer.TryGetValue(placeholder, LabelElementHelper.S_SQL_RES_COLUMN, out var sqlResColumn))
                    return false;

                var sqlPlaceHolder = new SqlPlaceHolder(placeholder, sql, new SqlResColumn(sqlResColumn));
                PlaceHolders.Add(sqlPlaceHolder);
            }

            Logger.Info($"Success to read {this.GetType().Name}", procName);
            return true;
        }
    }
}