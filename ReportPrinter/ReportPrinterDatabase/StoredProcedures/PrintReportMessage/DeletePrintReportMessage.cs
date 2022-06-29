using System;

namespace ReportPrinterDatabase.StoredProcedures.PrintReportMessage
{
    public class DeletePrintReportMessage : StoredProcedureBase
    {
        public DeletePrintReportMessage(Guid messageId)
        {
            Parameters.Add("@messageId", messageId);
        }
    }
}