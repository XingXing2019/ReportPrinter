using System.Collections.Generic;
using System.IO;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.SQL
{
    public class SqlTemplateManager : IXmlReader
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<string, SqlElementBase> _sqlTemplateList;

        private static SqlTemplateManager _instance;
        public static SqlTemplateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlTemplateManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private SqlTemplateManager()
        {
            _sqlTemplateList = new Dictionary<string, SqlElementBase>();
        }


        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var sqlTemplates = node.SelectNodes(XmlElementHelper.S_SQL_TEMPLATE);
            if (sqlTemplates == null || sqlTemplates.Count == 0)
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_SQL_TEMPLATE, node, procName);
                return false;
            }

            foreach (XmlNode sqlTemplateNode in sqlTemplates)
            {
                var path = sqlTemplateNode.InnerText;

                if (!File.Exists(path))
                {
                    Logger.Warn($"SQL template: {path} does not exist", procName);
                    continue;
                }

                var xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                var sqlTemplate = SqlElementFactory.CreateSqlElement(XmlElementHelper.S_SQL_TEMPLATE);

                if (!sqlTemplate.ReadXml(xmlDoc.DocumentElement))
                {
                    return false;
                }

                if (_sqlTemplateList.ContainsKey(sqlTemplate.Id))
                {
                    Logger.Error($"Duplicate SQL template id: {sqlTemplate.Id} detected", procName);
                    return false;
                }

                _sqlTemplateList.Add(sqlTemplate.Id, sqlTemplate);
            }

            if (_sqlTemplateList.Count == 0)
            {
                Logger.Error($"There is no valid sql template in the config", procName);
                return false;
            }

            Logger.Info($"Success to initialize sql template manager with {_sqlTemplateList.Count} sql template(s)", procName);
            return true;
        }

        /// <summary>
        /// Get a copy of sql
        /// </summary>
        /// <param name="sqlTemplateId"></param>
        /// <param name="sqlId"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool TryGetSql(string sqlTemplateId, string sqlId, out Sql sql)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetSql)}";
            sql = null;

            if (!_sqlTemplateList.ContainsKey(sqlTemplateId))
            {
                Logger.Error($"Sql template id: {sqlTemplateId} does not exist in sql template manager", procName);
                return false;
            }

            return ((SqlTemplate)_sqlTemplateList[sqlTemplateId]).TryGetSql(sqlId, out sql);
        }
    }
}