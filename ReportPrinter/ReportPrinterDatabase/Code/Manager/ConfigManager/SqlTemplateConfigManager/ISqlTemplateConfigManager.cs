﻿using ReportPrinterDatabase.Code.Model;
using System.Threading.Tasks;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager
{
    public interface ISqlTemplateConfigManager : IManager<SqlTemplateConfigModel>
    {
        Task PutSqlTemplateConfig(SqlTemplateConfigModel sqlTemplateConfig);
    }
}