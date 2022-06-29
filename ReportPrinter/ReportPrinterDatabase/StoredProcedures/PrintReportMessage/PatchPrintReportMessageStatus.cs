﻿using System;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace ReportPrinterDatabase.StoredProcedures.PrintReportMessage
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