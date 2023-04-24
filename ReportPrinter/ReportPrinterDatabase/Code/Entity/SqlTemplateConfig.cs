#nullable disable

using System;
using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlTemplateConfig
    {
        public SqlTemplateConfig()
        {
            SqlTemplateConfigSqlConfigs = new HashSet<SqlTemplateConfigSqlConfig>();
        }

        public Guid SqlTemplateConfigId { get; set; }
        public string Id { get; set; }

        public virtual ICollection<SqlTemplateConfigSqlConfig> SqlTemplateConfigSqlConfigs { get; set; }
    }
}
