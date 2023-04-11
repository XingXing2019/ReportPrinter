using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlConfig
    {
        public SqlConfig()
        {
            SqlTemplateConfigSqlConfigs = new HashSet<SqlTemplateConfigSqlConfig>();
            SqlVariableConfigs = new HashSet<SqlVariableConfig>();
        }

        public Guid SqlConfigId { get; set; }
        public string Id { get; set; }
        public string DatabaseId { get; set; }
        public string Query { get; set; }

        public virtual ICollection<SqlTemplateConfigSqlConfig> SqlTemplateConfigSqlConfigs { get; set; }
        public virtual ICollection<SqlVariableConfig> SqlVariableConfigs { get; set; }
    }
}
