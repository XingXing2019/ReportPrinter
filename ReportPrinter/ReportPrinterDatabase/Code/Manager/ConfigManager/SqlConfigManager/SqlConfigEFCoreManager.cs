﻿using System;
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
                var sqlConfig = await context.SqlConfigs
                    .Include(x => x.SqlVariableConfigs)
                    .FirstOrDefaultAsync(x => x.SqlConfigId == sqlConfigId);
                
                if (sqlConfig == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                    return null;
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
                await using var context = new ReportPrinterContext();
                var sqlConfigs = await context.SqlConfigs
                    .Include(x => x.SqlVariableConfigs)
                    .ToListAsync();

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
                await using var context = new ReportPrinterContext();
                var sqlConfig = await context.SqlConfigs.FindAsync(sqlConfigId);

                if (sqlConfig == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                }
                else
                {
                    context.SqlConfigs.Remove(sqlConfig);
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
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                await using var context = new ReportPrinterContext();
                
                var sqlConfigId = config.SqlConfigId;
                var sqlConfig = await context.SqlConfigs.Include(x => x.SqlVariableConfigs).FirstOrDefaultAsync(x => x.SqlConfigId == sqlConfigId);

                if (sqlConfig == null)
                {
                    Logger.Debug($"Sql config: {sqlConfigId} does not exist", procName);
                }
                else
                {
                    sqlConfig.Id = config.Id;
                    sqlConfig.DatabaseId = config.DatabaseId;
                    sqlConfig.Query = config.Query;

                    sqlConfig.SqlVariableConfigs = config.SqlVariableConfigs.Select(x => new SqlVariableConfig
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
                Logger.Error($"Exception happened during updating all Sql configs:. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}