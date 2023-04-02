using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlVariableConfig
    {
        public Guid SqlVariableId { get; set; }
        public Guid SqlId { get; set; }
        public string Name { get; set; }

        public virtual SqlConfig Sql { get; set; }
    }
}
