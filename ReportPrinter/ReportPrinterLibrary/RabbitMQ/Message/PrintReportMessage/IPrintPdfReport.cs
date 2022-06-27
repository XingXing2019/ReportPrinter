using System;
using System.Collections.Generic;

namespace ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage
{
    public interface IPrintPdfReport : IPrintReport
    {
        bool HasReprintFlag { get; }
    }

    public class PrintPdfReport : IPrintPdfReport
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public string TemplateId { get; set; }
        public string PrinterId { get; set; }
        public List<SqlVariable> SqlVariables { get; set; }
        public int NumberOfCopy { get; set; }
        public bool HasReprintFlag { get; set; }
    }
}