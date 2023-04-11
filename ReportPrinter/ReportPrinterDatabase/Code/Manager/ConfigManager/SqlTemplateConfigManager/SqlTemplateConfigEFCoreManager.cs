using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager
{
    public class SqlTemplateConfigEFCoreManager : ISqlTemplateConfigManager
    {
        public async Task Post(SqlTemplateConfigModel config)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var sqlTemplateConfig = new SqlTemplateConfig
                {
                    SqlTemplateConfigId = config.SqlTemplateConfigId,
                    Id = config.Id
                };

                var sqlTemplateConfigSqlConfigs = config.SqlConfigs
                    .Select(x => new SqlTemplateConfigSqlConfig
                    {
                        SqlTemplateConfigId = config.SqlTemplateConfigId,
                        SqlConfigId = x.SqlConfigId
                    });

                sqlTemplateConfig.SqlTemplateConfigSqlConfigs = sqlTemplateConfigSqlConfigs.ToList();

                context.SqlTemplateConfigs.Add(sqlTemplateConfig);
                var rows = await context.SaveChangesAsync();
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
                await using var context = new ReportPrinterContext();
                var entity = await context.SqlTemplateConfigs
                    .Include(x => x.SqlTemplateConfigSqlConfigs)
                    .ThenInclude(x => x.SqlConfig)
                    .ThenInclude(x => x.SqlVariableConfigs)
                    .FirstOrDefaultAsync(x => x.SqlTemplateConfigId == sqlTemplateConfigId);

                if (entity == null)
                {
                    Logger.Debug($"Sql template config: {sqlTemplateConfigId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve Sql template config: {sqlTemplateConfigId}", procName);

                var sqlTemplateConfig = CreateDataModel(entity);
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
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlTemplateConfigs
                    .Include(x => x.SqlTemplateConfigSqlConfigs)
                    .ThenInclude(x => x.SqlConfig)
                    .ThenInclude(x => x.SqlVariableConfigs)
                    .OrderBy(x => x.Id)
                    .ToListAsync();

                Logger.Debug($"Retrieve all sql template configs", procName);

                var sqlTemplateConfigs = entities.Select(CreateDataModel).ToList();
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
                await using var context = new ReportPrinterContext();
                var entity = await context.SqlTemplateConfigs.FindAsync(sqlTemplateConfigId);

                if (entity == null)
                {
                    Logger.Debug($"Sql template config: {sqlTemplateConfigId} does not exist", procName);
                }
                else
                {
                    context.SqlTemplateConfigs.Remove(entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete Sql config: {sqlTemplateConfigId}, {rows} row affected", procName);
                }
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
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlTemplateConfigs.Where(x => sqlTemplateConfigIds.Contains(x.SqlTemplateConfigId)).ToListAsync();

                if (entities.Count == 0)
                {
                    Logger.Debug($"Sql template configs does not exist", procName);
                }
                else
                {
                    context.SqlTemplateConfigs.RemoveRange(entities);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete Sql template configs, {rows} row affected", procName);
                }
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
                await using var context = new ReportPrinterContext();
                context.SqlTemplateConfigs.RemoveRange(context.SqlTemplateConfigs);
                var rows = await context.SaveChangesAsync();
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
                await using var context = new ReportPrinterContext();

                var sqlTemplateConfigId = sqlTemplateConfig.SqlTemplateConfigId;
                var entity = await context.SqlTemplateConfigs
                    .Include(x => x.SqlTemplateConfigSqlConfigs)
                    .ThenInclude(x => x.SqlConfig)
                    .FirstOrDefaultAsync(x => x.SqlTemplateConfigId == sqlTemplateConfigId);

                if (entity == null)
                {
                    Logger.Debug($"Sql template config: {sqlTemplateConfig} does not exist", procName);
                }
                else
                {
                    entity.Id = sqlTemplateConfig.Id;
                    var sqlTemplateConfigSqlConfigs = sqlTemplateConfig.SqlConfigs
                        .Select(x => new SqlTemplateConfigSqlConfig
                        {
                            SqlTemplateConfigId = sqlTemplateConfigId,
                            SqlConfigId = x.SqlConfigId
                        }).ToList();

                    entity.SqlTemplateConfigSqlConfigs = sqlTemplateConfigSqlConfigs;
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update Sql template config: {sqlTemplateConfig}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating Sql template configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<SqlTemplateConfigModel>> GetAllBySqlTemplateIdPrefix(string templateIdPrefix)
        {
            var procName = $"{this.GetType()}.{nameof(GetAllBySqlTemplateIdPrefix)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlTemplateConfigs
                    .Include(x => x.SqlTemplateConfigSqlConfigs)
                    .ThenInclude(x => x.SqlConfig)
                    .ThenInclude(x => x.SqlVariableConfigs)
                    .Where(x => x.Id.StartsWith(templateIdPrefix))
                    .ToListAsync();

                Logger.Debug($"Retrieve all sql configs by database id prefix: {templateIdPrefix}", procName);

                var sqlTemplateConfigs = entities.Select(CreateDataModel).ToList();
                return sqlTemplateConfigs;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all Sql template configs by template id prefix: {templateIdPrefix}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        private SqlTemplateConfigModel CreateDataModel(SqlTemplateConfig entity)
        {
            var sqlTemplateConfig = new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = entity.SqlTemplateConfigId,
                Id = entity.Id,
                SqlConfigs = entity.SqlTemplateConfigSqlConfigs.Select(sqlTemplateConfigSqlConfig => sqlTemplateConfigSqlConfig.SqlConfig).ToList()
            };

            return sqlTemplateConfig;
        }

        #endregion
    }
}