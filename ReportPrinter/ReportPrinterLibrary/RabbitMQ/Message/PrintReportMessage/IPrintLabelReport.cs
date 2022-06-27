﻿using System;
using System.Collections.Generic;

namespace ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage
{
    public interface IPrintLabelReport : IPrintReport { }

    public class PrintLabelReport : IPrintLabelReport
    {
        public Guid MessageId { get; set; }
        public Guid? CorrelationId { get; set; }
        public string ReportType { get; } = ReportTypeEnum.Label.ToString();
        public string TemplateId { get; set; }
        public string PrinterId { get; set; }
        public int NumberOfCopy { get; set; } = 1;
        public bool? HasReprintFlag { get; set; } = false;
        public DateTime? PublishTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string Status { get; set; }
        public List<SqlVariable> SqlVariables { get; set; }
    }
}