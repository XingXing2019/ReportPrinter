using System.Collections.Generic;
using System.Xml;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.SQL
{
    public class SqlTemplate : SqlElementBase
    {
        private Dictionary<string, SqlElementBase> _sqlList;

        public SqlTemplate()
        {
            _sqlList = new Dictionary<string, SqlElementBase>();
        }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";
            
            var id = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ID);
            if (string.IsNullOrEmpty(id))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ID, node, procName);
                return false;
            }
            Id = id;

            var sqls = node.SelectNodes(XmlElementHelper.S_SQL);
            if (sqls == null || sqls.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SQL, node, procName);
                return false;
            }

            foreach (XmlNode sqlNode in sqls)
            {
                var sql = SqlElementFactory.CreateSqlElement(XmlElementHelper.S_SQL);

                if (!sql.ReadXml(sqlNode))
                {
                    return false;
                }

                if (_sqlList.ContainsKey(sql.Id))
                {
                    Logger.Error($"Duplicate SQl id: {sql} detected", procName);
                    return false;
                }

                _sqlList.Add(sql.Id, sql);
            }

            Logger.Info($"Success to read sql template: {id} with {_sqlList.Count} sql(s)", procName);
            return true;
        }

        public override SqlElementBase Clone()
        {
            var cloned = (SqlTemplate)base.Clone();
            cloned._sqlList = new Dictionary<string, SqlElementBase>();
            foreach (var id in this._sqlList.Keys)
            {
                cloned._sqlList.Add(id, _sqlList[id].Clone());
            }

            return cloned;
        }

        public bool TryGetSql(string sqlId, out Sql sql)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetSql)}";
            sql = null;

            if (!_sqlList.ContainsKey(sqlId))
            {
                Logger.Error($"Sql id: {sqlId} does not exist in sql template", procName);
                return false;
            }

            sql = (Sql)_sqlList[sqlId].Clone();

            Logger.Debug($"Return a deep clone of sql: {sqlId}", procName);
            return true;
        }
    }
}