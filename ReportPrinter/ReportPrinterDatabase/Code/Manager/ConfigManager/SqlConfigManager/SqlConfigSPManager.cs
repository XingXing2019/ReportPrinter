﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.SqlConfig;
using ReportPrinterDatabase.Code.StoredProcedures.SqlVariableConfig;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager
{
    public class SqlConfigSPManager : ISqlConfigManager
    {
        private readonly StoredProcedureExecutor _executor;

        public SqlConfigSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(SqlConfig config)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var sqlConfigId = config.SqlConfigId;
                var id = config.Id;
                var databaseId = config.DatabaseId;
                var query = config.Query;
                var sqlVariableNames = string.Join(',', config.SqlVariableConfigs.Select(x => x.Name));

                var sp = new PostSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                var rows = await _executor.ExecuteNonQueryAsync(sp);

                Logger.Debug($"Record Sql config: {config.SqlConfigId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording Sql config: {config.SqlConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<SqlConfig> Get(Guid sqlConfigId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var sqlConfig = await _executor.ExecuteQueryOneAsync<SqlConfig>(new GetSqlConfig(sqlConfigId));
                var sqlVariableConfigs = await _executor.ExecuteQueryBatchAsync<SqlVariableConfig>(new GetSqlVariableConfig(sqlConfigId));

                if (sqlConfig != null)
                {
                    sqlConfig.SqlVariableConfigs = sqlVariableConfigs;
                }

                Logger.Debug($"Retrieve Sql config: {sqlConfigId}", procName);
                return sqlConfig;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving Sql config: {sqlConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IList<SqlConfig>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            try
            {
                var sqlConfigs = await _executor.ExecuteQueryBatchAsync<SqlConfig>(new GetAllSqlConfig());
                var sqlVariableConfigs = await _executor.ExecuteQueryBatchAsync<SqlVariableConfig>(new GetAllSqlVariableConfig());

                foreach (var sqlConfig in sqlConfigs)
                {
                    sqlConfig.SqlVariableConfigs = sqlVariableConfigs.Where(x => x.SqlConfigId == sqlConfig.SqlConfigId).ToList();
                }

                Logger.Debug($"Retrieve all sql configs", procName);
                return sqlConfigs;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all Sql configs. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(Guid sqlConfigId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteSqlConfig(sqlConfigId));
                Logger.Debug($"Delete Sql config: {sqlConfigId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting Sql config: {sqlConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteAllSqlConfig());
                Logger.Debug($"Delete all Sql configs, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting all Sql configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PutSqlConfig(SqlConfig config)
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                var sqlConfigId = config.SqlConfigId;
                var id = config.Id;
                var databaseId = config.DatabaseId;
                var query = config.Query;
                var sqlVariableNames = string.Join(',', config.SqlVariableConfigs.Select(x => x.Name));

                var sp = new PutSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                var rows = await _executor.ExecuteNonQueryAsync(sp);

                Logger.Debug($"Update Sql config: {sqlConfigId}, {rows} row affected", procName);

            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating all Sql configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}