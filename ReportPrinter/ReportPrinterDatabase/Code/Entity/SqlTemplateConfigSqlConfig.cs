using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlTemplateConfigSqlConfig
    {
        public Guid SqlTemplateConfigId { get; set; }
        public Guid SqlConfigId { get; set; }

        public virtual SqlConfig SqlConfig { get; set; }
        public virtual SqlTemplateConfig SqlTemplateConfig { get; set; }
    }
}
