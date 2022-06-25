using System;
using System.Collections.Generic;
using MassTransit;

namespace ReportPrinterLibrary.RabbitMQ.Message
{
    public interface IPrintReport : CorrelatedBy<Guid>
    {
        Guid CorrelationId { get; }
        Guid MessageId { get; }
        string TemplateId { get; }
        string PrinterId { get; }
        List<SqlVariable> SqlVariables { get; }
        int NumberOfCopy { get; }
    }

    public class SqlVariable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}