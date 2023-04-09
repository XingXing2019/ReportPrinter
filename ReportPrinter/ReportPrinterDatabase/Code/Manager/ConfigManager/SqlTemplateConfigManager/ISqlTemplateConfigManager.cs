using ReportPrinterDatabase.Code.Entity;
using System.Threading.Tasks;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager
{
    public interface ISqlTemplateConfigManager : IManager<SqlTemplateConfig>
    {
        Task PutSqlTemplateConfig(SqlTemplateConfig sqlTemplateConfig);
    }
}