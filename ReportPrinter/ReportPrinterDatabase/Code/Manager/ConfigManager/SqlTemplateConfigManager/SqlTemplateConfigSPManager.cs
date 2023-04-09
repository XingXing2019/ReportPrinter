using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.SqlConfig;
using ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager
{
    public class SqlTemplateConfigSPManager : ISqlTemplateConfigManager
    {
        private readonly StoredProcedureExecutor _executor;

        public SqlTemplateConfigSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(SqlTemplateConfigModel config)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var sqlTemplateConfigId = config.SqlTemplateConfigId;
                var id = config.Id;
                var sqlConfigIds = string.Join(',', config.SqlConfigs.Select(x => x.SqlConfigId));

                var sp = new PostSqlTemplateConfig(sqlTemplateConfigId, id, sqlConfigIds);
                var rows = await _executor.ExecuteNonQueryAsync(sp);

                Logger.Debug($"Record Sql template config: {config.SqlTemplateConfigId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording Sql template config: {config.SqlTemplateConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<SqlTemplateConfigModel> Get(Guid sqlTemplateConfigId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var sqlTemplateConfig = await _executor.ExecuteQueryOneAsync<SqlTemplateConfigModel>(new GetSqlTemplateConfig(sqlTemplateConfigId));
                var sqlConfigs = await _executor.ExecuteQueryBatchAsync<SqlConfig>(new GetSqlConfigsBySqlTemplateConfigId(sqlTemplateConfigId));

                if (sqlTemplateConfig != null)
                {
                    sqlTemplateConfig.SqlConfigs = sqlConfigs;
                }

                Logger.Debug($"Retrieve Sql template config: {sqlTemplateConfigId}", procName);
                return sqlTemplateConfig;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving Sql template config: {sqlTemplateConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IList<SqlTemplateConfigModel>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            try
            {
                var sqlTemplateConfigs = await _executor.ExecuteQueryBatchAsync<SqlTemplateConfigModel>(new GetAllSqlTemplateConfig());
                var sqlConfigs = await _executor.ExecuteQueryBatchAsync<SqlConfig>(new GetAllSqlConfig());
                var sqlTemplateConfigSqlConfig = await _executor.ExecuteQueryBatchAsync<SqlTemplateConfigSqlConfig>(new GetAllSqlTemplateConfigSqlConfig());

                var dict = sqlTemplateConfigSqlConfig.GroupBy(x => x.SqlTemplateConfigId).ToDictionary(x => x.Key, x => x.Select(y => sqlConfigs.Single(z => z.SqlConfigId == y.SqlConfigId)));

                foreach (var sqlTemplateConfig in sqlTemplateConfigs)
                {
                    sqlTemplateConfig.SqlConfigs = dict[sqlTemplateConfig.SqlTemplateConfigId].ToList();
                }

                Logger.Debug($"Retrieve all sql template configs", procName);
                return sqlTemplateConfigs;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all Sql template configs. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(Guid sqlTemplateConfigId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteSqlTemplateConfigById(sqlTemplateConfigId));
                Logger.Debug($"Delete Sql config: {sqlTemplateConfigId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting Sql template config: {sqlTemplateConfigId}. Ex: {ex.Message}", procName);
                throw;
            }

        }

        public async Task Delete(List<Guid> sqlTemplateConfigIds)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteSqlTemplateConfigByIds(string.Join(',', sqlTemplateConfigIds)));
                Logger.Debug($"Delete Sql template configs, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting Sql template configs. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteAllSqlTemplateConfig());
                Logger.Debug($"Delete all Sql template configs, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting all Sql template configs. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PutSqlTemplateConfig(SqlTemplateConfigModel sqlTemplateConfig)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutSqlTemplateConfig)}";

            try
            {
                var sqlTemplateConfigId = sqlTemplateConfig.SqlTemplateConfigId;
                var id = sqlTemplateConfig.Id;
                var sqlConfigIds = string.Join(',', sqlTemplateConfig.SqlConfigs.Select(x => x.SqlConfigId));

                var rows = await _executor.ExecuteNonQueryAsync(new PutSqlTemplateConfig(sqlTemplateConfigId, id, sqlConfigIds));
                Logger.Debug($"Update Sql template config: {sqlTemplateConfig}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating Sql template configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}