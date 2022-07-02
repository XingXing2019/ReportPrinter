using System.Collections.Generic;
using System.Linq;
using System.Xml;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
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

            var id = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_ID);
            if (string.IsNullOrEmpty(id))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_ID, node, procName);
                return false;
            }
            Id = id;

            var databaseId = XmlElementHelper.GetAttribute(node, XmlElementHelper.S_DATABASE_ID);
            if (string.IsNullOrEmpty(databaseId))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_DATABASE_ID, node, procName);
                return false;
            }
            _databaseId = databaseId;

            var query = node.SelectSingleNode(XmlElementHelper.S_QUERY)?.InnerText;
            if (string.IsNullOrEmpty(query))
            {
                Logger.LogMissingXmlLog(XmlElementHelper.S_QUERY, node, procName);
                return false;
            }
            _query = query;

            var variables = node.SelectNodes(XmlElementHelper.S_VARIABLE);
            if (variables == null || variables.Count == 0)
            {
                Logger.Debug($"SQL: {id} does not need variable to execute", procName);
            }
            else
            {
                foreach (XmlNode variableNode in variables)
                {
                    var name = XmlElementHelper.GetAttribute(variableNode, XmlElementHelper.S_NAME);
                    if (string.IsNullOrEmpty(name))
                    {
                        Logger.LogMissingXmlLog(XmlElementHelper.S_NAME, node, procName);
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
                cloned._sqlVariables.Add(id, _sqlVariables[id].Clone());
            }

            return cloned;
        }
    }
}