﻿#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PrintReportSqlVariable
    {
        public Guid SqlVariableId { get; set; }
        public Guid MessageId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual PrintReportMessage Message { get; set; }
    }
}
