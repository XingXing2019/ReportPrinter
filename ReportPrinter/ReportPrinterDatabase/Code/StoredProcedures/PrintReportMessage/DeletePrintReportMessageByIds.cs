namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
{
    public class DeletePrintReportMessageByIds : StoredProcedureBase
    {
        public DeletePrintReportMessageByIds(string messageIds)
        {
            Parameters.Add("@messageIds", messageIds);
        }
    }
}