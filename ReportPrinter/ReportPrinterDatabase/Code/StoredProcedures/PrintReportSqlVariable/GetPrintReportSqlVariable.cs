using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportSqlVariable
{
    public class GetPrintReportSqlVariable : StoredProcedureBase
    {
        public GetPrintReportSqlVariable(Guid messageId)
        {
            Parameters.Add("@messageId", messageId);
        }
    }
}