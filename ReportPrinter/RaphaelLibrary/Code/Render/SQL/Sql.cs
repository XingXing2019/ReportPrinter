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

            var id = node.Attributes?[XmlElementName.S_ID]?.Value;
            if (string.IsNullOrEmpty(id))
            {
                Logger.LogMissingXmlLog(XmlElementName.S_ID, node, procName);
                return false;
            }
            Id = id;

            var databaseId = node.Attributes?[XmlElementName.S_DATABASE_ID]?.Value;
            if (string.IsNullOrEmpty(databaseId))
            {
                Logger.LogMissingXmlLog(XmlElementName.S_DATABASE_ID, node, procName);
                return false;
            }
            _databaseId = databaseId;

            var query = node.SelectSingleNode(XmlElementName.S_QUERY)?.InnerText;
            if (string.IsNullOrEmpty(query))
            {
                Logger.LogMissingXmlLog(XmlElementName.S_QUERY, node, procName);
                return false;
            }
            _query = query;

            var variables = node.SelectNodes(XmlElementName.S_VARIABLE);
            if (variables == null || variables.Count == 0)
            {
                Logger.Debug($"SQL: {id} does not need variable to execute", procName);
            }
            else
            {
                foreach (XmlNode variableNode in variables)
                {
                    var name = variableNode.Attributes?[XmlElementName.S_NAME]?.Value;
                    if (string.IsNullOrEmpty(name))
                    {
                        Logger.LogMissingXmlLog(XmlElementName.S_NAME, node, procName);
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

            Logger.Debug($"Success to read sql: {id}, database id: {_databaseId}, " +
                         $"variables: {string.Join(',', _sqlVariables.Select(x => x.Key))}, query: \n{_query}", procName);
            return true;
        }

        public override SqlElementBase Clone()
        {
            var cloned = (Sql)base.Clone();
            cloned._sqlVariables = new Dictionary<string, SqlVariable>();
            foreach (var id in this._sqlVariables.Keys)
            {
                cloned._sqlVariables.Add(id, (SqlVariable)_sqlVariables[id].Clone());
            }

            return cloned;
        }
    }
}