using System.Collections.Generic;

namespace ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage
{
    public interface IPrintReport : IMessage
    {
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