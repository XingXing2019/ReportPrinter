using System;
using System.Collections.Generic;
using ReportPrinterDatabase.Code.Entity;

namespace ReportPrinterDatabase.Code.Model
{
    public class SqlTemplateConfigModel
    {
        public Guid SqlTemplateConfigId { get; set; }
        public string Id { get; set; }

        public List<SqlConfig> SqlConfigs { get; set; }
    }
}