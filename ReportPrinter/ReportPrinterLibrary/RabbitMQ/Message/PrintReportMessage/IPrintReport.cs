using System;
using System.Collections.Generic;

namespace ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage
{
    public interface IPrintReport : IMessage
    {
        public string ReportType { get; }
        string TemplateId { get; set; }
        string PrinterId { get; set; }
        int NumberOfCopy { get; set; }
        bool? HasReprintFlag { get; set; }
        public DateTime? PublishTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string Status { get; set; }
        List<SqlVariable> SqlVariables { get; set; }
    }

    public class SqlVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public enum ReportTypeEnum
    {
        PDF,
        Label
    }
}