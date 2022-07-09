using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using Microsoft.Data.SqlClient;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Render.PDF.Helper;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Render.SQL
{
    public class Sql : SqlElementBase, ISqlExecutor
    {
        private string _connectionString;
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

            if (!DatabaseManager.Instance.TryGetConnectionString(databaseId, out var connectionString))
            {
                return false;
            }
            _connectionString = connectionString;

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

            Logger.Debug($"Success to read sql: {id}, database id: {databaseId}, " +
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
        
        public bool TryExecute(Guid messageId, SqlResColumn sqlResColumn, out string res)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryExecute)}";
            res = string.Empty;

            if (!TrySetSqlVariables(messageId, out var sqlVariables))
                return false;

            if (!TryExecuteQuery(messageId, _connectionString, _query, sqlVariables, out var dataTable))
                return false;

            if (dataTable.Rows.Count != 1)
            {
                Logger.Error($"More than 1 row returned from sql: {Id}", procName);
            }

            var index = dataTable.Columns.IndexOf(sqlResColumn.Id);
            if (index == -1)
            {
                Logger.Error($"Sql res column: {sqlResColumn} is not returned from sql: {Id}", procName);
            }
            res = dataTable.Rows[0][index].ToString()?.Trim();

            return true;
        }

        public bool TryExecute(Guid messageId, List<SqlResColumn> sqlResColumnList, out List<Dictionary<string, string>> res)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryExecute)}";
            res = new List<Dictionary<string, string>>();

            if (!TrySetSqlVariables(messageId, out var sqlVariables))
                return false;

            if (!TryExecuteQuery(messageId, _connectionString, _query, sqlVariables, out var dataTable))
                return false;

            var indexDict = new Dictionary<int, string>();
            foreach (var sqlResColumn in sqlResColumnList)
            {
                var index = dataTable.Columns.IndexOf(sqlResColumn.Id);
                if (index == -1)
                {
                    Logger.Error($"Unable to locate sql result column: {sqlResColumn.Id} in the result", procName);
                    return false;
                }

                indexDict.Add(index, sqlResColumn.Id);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var resRow = new Dictionary<string, string>();
                for (int i = 0; i < row.ItemArray.Length; i++)
                {
                    if (!indexDict.ContainsKey(i))
                        continue;
                    resRow.Add(indexDict[i], row.ItemArray[i].ToString()?.Trim());
                }

                res.Add(resRow);
            }

            return true;
        }


        #region Helper

        private bool TrySetSqlVariables(Guid messageId, out Dictionary<string, SqlVariable> sqlVariables)
        {
            var procName = $"{this.GetType().Name}.{nameof(TrySetSqlVariables)}";
            sqlVariables = null;

            var values = SqlVariableManager.Instance.GetSqlVariables(messageId);
            if (_sqlVariables.Any(x => !values.ContainsKey(x.Key)))
            {
                Logger.Error($"Sql variables provided in message: {messageId} does not fulfill requirement of sql: {Id}", procName);
                return false;
            }

            sqlVariables = new Dictionary<string, SqlVariable>();
            foreach (var variable in _sqlVariables.Keys)
            {
                sqlVariables.Add(variable, values[variable]);
            }

            return true;
        }

        private bool TryExecuteQuery(Guid messageId, string connectionString, string query, Dictionary<string, SqlVariable> sqlVariables, out DataTable dataTable)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryExecuteQuery)}";

            if (SqlResultCacheManager.Instance.TryGetSqlResult(messageId, Id, out dataTable))
            {
                return true;
            }

            dataTable = null;
            try
            {
                using var sqlConnection = new SqlConnection(connectionString);
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                query = ReplacePlaceHolder(query, sqlVariables);
                Logger.Debug($"Try to execute sql: \n{query}", procName);
                var cmd = sqlConnection.CreateCommand();
                cmd.CommandText = query;
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Logger.Error($"0 row return from sql: {Id}", procName);
                    return false;
                }

                dataTable = new DataTable();
                dataTable.Load(reader);
                Logger.Debug($"Success to execute sql: {Id}. {dataTable.Rows.Count} row(s) returned", procName);
                
                SqlResultCacheManager.Instance.StoreSqlResult(messageId, Id, dataTable);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during executing sql: {Id}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        private string ReplacePlaceHolder(string query, Dictionary<string, SqlVariable> sqlVariables)
        {
            foreach (var variable in sqlVariables.Keys)
            {
                query = query.Replace($"%%%{variable}%%%", $"{sqlVariables[variable].Value}");
            }

            return query;
        }
        
        #endregion
    }
}