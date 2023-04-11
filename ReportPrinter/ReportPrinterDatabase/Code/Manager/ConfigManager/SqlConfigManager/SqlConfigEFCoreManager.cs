using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterLibrary.Code.Log;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager
{
    public class SqlConfigEFCoreManager : ISqlConfigManager
    {
        public async Task Post(SqlConfig config)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();
                
                var sqlConfig = new SqlConfig
                {
                    SqlConfigId = config.SqlConfigId,
                    Id = config.Id,
                    DatabaseId = config.DatabaseId,
                    Query = config.Query,
                };

                var sqlVariableConfigs = config.SqlVariableConfigs.Select(x => new SqlVariableConfig
                {
                    SqlConfigId = config.SqlConfigId,
                    Name = x.Name,
                }).ToList();

                sqlConfig.SqlVariableConfigs = sqlVariableConfigs;
                context.SqlConfigs.Add(sqlConfig);
                var rows = await context.SaveChangesAsync();
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
                await using var context = new ReportPrinterContext();
                var entity = await context.SqlConfigs
                    .Include(x => x.SqlVariableConfigs)
                    .FirstOrDefaultAsync(x => x.SqlConfigId == sqlConfigId);
                
                if (entity == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve Sql config: {sqlConfigId}", procName);
                return entity;
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
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlConfigs
                    .Include(x => x.SqlVariableConfigs)
                    .OrderBy(x => x.DatabaseId)
                    .ToListAsync();

                Logger.Debug($"Retrieve all sql configs", procName);
                return entities;
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
                await using var context = new ReportPrinterContext();
                var entity = await context.SqlConfigs.FindAsync(sqlConfigId);

                if (entity == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                }
                else
                {
                    context.SqlConfigs.Remove(entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete Sql config: {sqlConfigId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting Sql config: {sqlConfigId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(List<Guid> sqlConfigIds)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlConfigs.Where(x => sqlConfigIds.Contains(x.SqlConfigId)).ToListAsync();

                if (entities.Count == 0)
                {
                    Logger.Debug($"Sql configs does not exist", procName);
                }
                else
                {
                    context.SqlConfigs.RemoveRange(entities);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete Sql configs, {rows} row affected", procName); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting Sql configs. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                await using var context = new ReportPrinterContext();
                context.SqlConfigs.RemoveRange(context.SqlConfigs);
                var rows = await context.SaveChangesAsync();
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
            var procName = $"{this.GetType().Name}.{nameof(PutSqlConfig)}";

            try
            {
                await using var context = new ReportPrinterContext();
                
                var sqlConfigId = config.SqlConfigId;
                var entity = await context.SqlConfigs.Include(x => x.SqlVariableConfigs).FirstOrDefaultAsync(x => x.SqlConfigId == sqlConfigId);

                if (entity == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                }
                else
                {
                    entity.Id = config.Id;
                    entity.DatabaseId = config.DatabaseId;
                    entity.Query = config.Query;

                    entity.SqlVariableConfigs = config.SqlVariableConfigs.Select(x => new SqlVariableConfig
                    {
                        SqlVariableConfigId = x.SqlVariableConfigId,
                        SqlConfigId = config.SqlConfigId,
                        Name = x.Name,
                    }).ToList();

                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update Sql config: {sqlConfigId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating Sql configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<SqlConfig>> GetAllByDatabaseIdPrefix(string databaseIdPrefix)
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAllByDatabaseIdPrefix)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.SqlConfigs
                    .Include(x => x.SqlVariableConfigs)
                    .Where(x => x.DatabaseId.StartsWith(databaseIdPrefix))
                    .OrderBy(x => x.DatabaseId)
                    .ToListAsync();

                Logger.Debug($"Retrieve all sql configs by database id prefix: {databaseIdPrefix}", procName);
                return entities;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all Sql configs by database Id prefix: {databaseIdPrefix}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}