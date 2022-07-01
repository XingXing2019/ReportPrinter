using System.Collections.Generic;
using System.Xml;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.SQL
{
    public class SqlTemplate : SqlElementBase
    {
        private Dictionary<string, SqlElementBase> _sqlLists;

        public SqlTemplate()
        {
            _sqlLists = new Dictionary<string, SqlElementBase>();
        }

        public override bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var id = node.Attributes?[XmlElementName.ID]?.Value;
            if (string.IsNullOrEmpty(id))
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.ID, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }
            Id = id;

            var sqls = node.SelectNodes(XmlElementName.SQL);
            if (sqls == null || sqls.Count == 0)
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.SQL, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }

            foreach (XmlNode sqlNode in sqls)
            {
                var sql = SqlElementFactory.CreateSqlElement(XmlElementName.SQL);

                if (!sql.ReadXml(sqlNode))
                {
                    return false;
                }

                if (_sqlLists.ContainsKey(sql.Id))
                {
                    Logger.Error($"Duplicate SQl id: {sql} detected", procName);
                    return false;
                }

                _sqlLists.Add(sql.Id, sql);
            }

            Logger.Debug($"Success to read sql template: {id} with {_sqlLists.Count} sql(s)", procName);
            return true;
        }

        public override SqlElementBase Clone()
        {
            var sqlTemplate = (SqlTemplate)base.Clone();
            sqlTemplate._sqlLists = new Dictionary<string, SqlElementBase>();
            foreach (var id in this._sqlLists.Keys)
            {
                sqlTemplate._sqlLists.Add(id, _sqlLists[id].Clone());
            }

            return sqlTemplate;
        }

        public bool TryGetSql(string sqlId, out Sql sql)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetSql)}";
            sql = null;

            if (!_sqlLists.ContainsKey(sqlId))
            {
                Logger.Error($"Sql id: {sqlId} does not exist in sql template", procName);
                return false;
            }

            sql = (Sql)_sqlLists[sqlId].Clone();

            Logger.Debug($"Return a deep clone of sql: {sqlId}", procName);
            return true;
        }
    }
}