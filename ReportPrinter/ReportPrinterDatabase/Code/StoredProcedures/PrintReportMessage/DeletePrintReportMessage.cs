using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
{
    public class DeletePrintReportMessage : StoredProcedureBase
    {
        public DeletePrintReportMessage(Guid messageId)
        {
            Parameters.Add("@messageId", messageId);
        }
    }
}