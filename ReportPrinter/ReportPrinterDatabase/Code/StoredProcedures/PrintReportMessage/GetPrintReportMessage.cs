using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
{
    public class GetPrintReportMessage : StoredProcedureBase
    {
        public GetPrintReportMessage(Guid messageId)
        {
            Parameters.Add("@messageId", messageId);
        }
    }
}