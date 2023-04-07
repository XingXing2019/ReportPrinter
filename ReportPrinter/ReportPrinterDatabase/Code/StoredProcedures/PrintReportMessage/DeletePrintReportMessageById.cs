using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
{
    public class DeletePrintReportMessageById : StoredProcedureBase
    {
        public DeletePrintReportMessageById(Guid messageId)
        {
            Parameters.Add("@messageId", messageId);
        }
    }
}