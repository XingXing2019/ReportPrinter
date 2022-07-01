using System.Collections.Generic;
using System.Linq;
using System.Xml;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Render.SQL
{
    public class Sql : SqlElementBase
    {
        private string _databaseId;
        private string _query;
        private Dictionary<string, SqlVariable> _sqlVariables;

        public Sql()
        {
            _sqlVariables = new Dictionary<string, SqlVariable>();
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

            var databaseId = node.Attributes?[XmlElementName.DATABASE_ID]?.Value;
            if (string.IsNullOrEmpty(databaseId))
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.DATABASE_ID, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }
            _databaseId = databaseId;

            var query = node.SelectSingleNode(XmlElementName.QUERY)?.InnerText;
            if (string.IsNullOrEmpty(query))
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.QUERY, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }
            _query = query;

            var variables = node.SelectNodes(XmlElementName.VARIABLE);
            if (variables == null || variables.Count == 0)
            {
                Logger.Debug($"SQL: {id} does not need variable to execute", procName);
            }
            else
            {
                foreach (XmlNode variableNode in variables)
                {
                    var name = variableNode.Attributes?[XmlElementName.NAME]?.Value;
                    if (string.IsNullOrEmpty(name))
                    {
                        var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.NAME, node);
                        Logger.Error(missingXmlLog, procName);
                        return false;
                    }

                    if (_sqlVariables.ContainsKey(name))
                    {
                        Logger.Error($"Duplicate variable name: {name} detected", procName);
                        return false;
                    }

                    _sqlVariables.Add(name, new SqlVariable { Name = name });
                }
            }

            Logger.Debug($"Success to read sql: {id}, variables: {string.Join(',', _sqlVariables.Select(x => x.Key))}, query: \n{query}", procName);
            return true;
        }

        public override SqlElementBase Clone()
        {
            var sql = (Sql)base.Clone();
            sql._sqlVariables = new Dictionary<string, SqlVariable>();
            foreach (var id in this._sqlVariables.Keys)
            {
                sql._sqlVariables.Add(id, (SqlVariable)_sqlVariables[id].Clone());
            }

            return sql;
        }
    }
}