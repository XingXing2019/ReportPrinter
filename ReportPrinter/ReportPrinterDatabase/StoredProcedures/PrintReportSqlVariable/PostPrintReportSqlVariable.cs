using System;

namespace ReportPrinterDatabase.StoredProcedures.PrintReportSqlVariable
{
    public class PostPrintReportSqlVariable : StoredProcedureBase
    {
        public PostPrintReportSqlVariable(Guid messageId, string name, string value)
        {
            Parameters.Add("@messageId", messageId);
            Parameters.Add("@name", name);
            Parameters.Add("@value", value);
        }
    }
}