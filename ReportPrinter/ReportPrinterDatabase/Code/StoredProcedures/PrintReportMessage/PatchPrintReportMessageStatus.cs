using System;
using ReportPrinterLibrary.Code.RabbitMQ.Message;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
{
    public class PatchPrintReportMessageStatus : StoredProcedureBase
    {
        public PatchPrintReportMessageStatus(Guid messageId, MessageStatus status)
        {
            Parameters.Add("@messageId", messageId);
            Parameters.Add("@status", status.ToString());
        }
    }
}