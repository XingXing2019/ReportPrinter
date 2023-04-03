using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlVariableConfig
    {
        public Guid SqlVariableConfigId { get; set; }
        public Guid SqlConfigId { get; set; }
        public string Name { get; set; }

        public virtual SqlConfig SqlConfig { get; set; }
    }
}
