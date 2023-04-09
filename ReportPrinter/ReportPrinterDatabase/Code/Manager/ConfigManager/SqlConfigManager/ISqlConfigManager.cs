using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Entity;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager
{
    public interface ISqlConfigManager : IManager<SqlConfig>
    {
        Task PutSqlConfig(SqlConfig sqlConfig);
        Task<List<SqlConfig>> GetAllByDatabaseIdPrefix(string databaseIdPrefix);
    }
}