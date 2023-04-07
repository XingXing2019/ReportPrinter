using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Executor
{
    public class StoredProcedureExecutor
    {
        private readonly string _connectionString;

        public StoredProcedureExecutor()
        {
            var databaseId = AppConfig.Instance.TargetDatabase;
            if (!DatabaseManager.Instance.TryGetConnectionString(databaseId, out var connectionString))
            {
                throw new ApplicationException($"Database connection: {databaseId} does not exist");
            }

            _connectionString = connectionString;
        }
        
        public async Task<int> ExecuteNonQueryAsync(params StoredProcedureBase[] storedProcedures)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var rows = 0;

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            foreach (var storedProcedure in storedProcedures)
            {
                var cmd = new SqlCommand(storedProcedure.StoredProcedureName, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in storedProcedure.Parameters.Keys)
                {
                    cmd.Parameters.AddWithValue(parameter, storedProcedure.Parameters[parameter]);
                }

                rows += await cmd.ExecuteNonQueryAsync();
                LogExecutedStoredProcedure(cmd);
            }

            scope.Complete();
            return rows;
        }

        public async Task<T> ExecuteQueryOneAsync<T>(StoredProcedureBase storedProcedure)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand(storedProcedure.StoredProcedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in storedProcedure.Parameters.Keys)
            {
                cmd.Parameters.AddWithValue(parameter, storedProcedure.Parameters[parameter]);
            }

            var reader = await cmd.ExecuteReaderAsync();
            LogExecutedStoredProcedure(cmd);

            var dataTable = new DataTable();
            dataTable.Load(reader);

            if (dataTable.Rows.Count == 0)
            {
                return default;
            }

            if (dataTable.Rows.Count > 1)
            {
                throw new DataException($"More than 1 result returned from stored procedure: {storedProcedure.StoredProcedureName}");
            }

            var row = dataTable.Rows[0];
            var columns = dataTable.Columns;

            var entity = (T)MapToEntity(typeof(T), row, columns);

            scope.Complete();
            return entity;
        }

        public async Task<List<T>> ExecuteQueryBatchAsync<T>(StoredProcedureBase storedProcedure)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            var cmd = new SqlCommand(storedProcedure.StoredProcedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var parameter in storedProcedure.Parameters.Keys)
            {
                cmd.Parameters.AddWithValue(parameter, storedProcedure.Parameters[parameter]);
            }

            var reader = await cmd.ExecuteReaderAsync();
            LogExecutedStoredProcedure(cmd);

            var dataTable = new DataTable();
            dataTable.Load(reader);

            var columns = dataTable.Columns;
            var type = typeof(T);

            var entities = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var entity = (T)MapToEntity(type, row, columns);
                entities.Add(entity);
            }

            scope.Complete();
            return entities;
        }


        #region Helper

        private object MapToEntity(Type type, DataRow row, DataColumnCollection columns)
        {
            var propInfos = type.GetProperties();

            var entity = Activator.CreateInstance(type);
            foreach (var propInfo in propInfos)
            {
                if (!columns.Contains(propInfo.Name)) continue;
                var value = row[propInfo.Name];
                if (value == DBNull.Value) continue;
                propInfo.SetValue(entity, value);
            }

            return entity;
        }

        private void LogExecutedStoredProcedure(SqlCommand cmd)
        {
            var procName = $"{this.GetType().Name}.{nameof(LogExecutedStoredProcedure)}";
            
            var storedProcedureName = cmd.CommandText;
            var parameters = new List<string>();

            foreach (SqlParameter parameter in cmd.Parameters)
            {
                var val = parameter.Value;
                if (val == DBNull.Value)
                    parameters.Add("NULL");
                else if (val is string || val is DateTime || val is Guid)
                    parameters.Add($"'{val}'");
                else if (val is bool)
                    parameters.Add((bool)val ? "1" : "0");
                else 
                    parameters.Add(val.ToString());
            }

            var script = $"EXEC [dbo].[{storedProcedureName}] {string.Join(", ", parameters)}";
            Logger.Debug($"Execute script: {script}", procName);
        }

        #endregion
    }
}