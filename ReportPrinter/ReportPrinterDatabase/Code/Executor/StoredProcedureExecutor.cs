﻿using System.Data;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace ReportPrinterDatabase.Code.Executor
{
    public class StoredProcedureExecutor
    {
        private readonly string _connectionString;

        public StoredProcedureExecutor()
        {
            var databaseName = AppConfig.Instance.TargetDatabase;
            _connectionString = DatabaseManager.Instance.GetConnectionString(databaseName);
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
            }

            scope.Complete();
            return rows;
        }

        public async Task<T> ExecuteQueryAsync<T>(StoredProcedureBase storedProcedure)
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

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    

                }
            }
            else
            {
                return default;
            }

            return default;
        }
    }
}