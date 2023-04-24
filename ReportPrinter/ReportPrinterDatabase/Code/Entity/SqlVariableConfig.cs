#nullable disable

using System;

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
