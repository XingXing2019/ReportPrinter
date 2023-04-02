﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlConfig
    {
        public SqlConfig()
        {
            SqlVariableConfigs = new HashSet<SqlVariableConfig>();
        }

        public Guid SqlId { get; set; }
        public string Id { get; set; }
        public string DatabaseId { get; set; }
        public string Query { get; set; }

        public virtual ICollection<SqlVariableConfig> SqlVariableConfigs { get; set; }
    }
}
