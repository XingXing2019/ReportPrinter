﻿using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage
{
    public interface IPrintReport : IMessage
    {
        public ReportTypeEnum ReportType { get; }
        string TemplateId { get; set; }
        string PrinterId { get; set; }
        int NumberOfCopy { get; set; }
        bool? HasReprintFlag { get; set; }
        public DateTime? PublishTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string Status { get; set; }
        List<SqlVariable> SqlVariables { get; set; }

        bool IsValid { get; }
    }

    [Serializable]
    public class SqlVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value);
        public SqlVariable Clone()
        {
            return this.MemberwiseClone() as SqlVariable;
        }
    }
}