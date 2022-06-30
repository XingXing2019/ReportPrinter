using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PrintReportMessage
    {
        public PrintReportMessage()
        {
            PrintReportSqlVariables = new HashSet<PrintReportSqlVariable>();
        }

        public Guid MessageId { get; set; }
        public Guid? CorrelationId { get; set; }
        public string ReportType { get; set; }
        public string TemplateId { get; set; }
        public string PrinterId { get; set; }
        public int NumberOfCopy { get; set; }
        public bool? HasReprintFlag { get; set; }
        public DateTime? PublishTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string Status { get; set; }

        public virtual ICollection<PrintReportSqlVariable> PrintReportSqlVariables { get; set; }
    }
}
