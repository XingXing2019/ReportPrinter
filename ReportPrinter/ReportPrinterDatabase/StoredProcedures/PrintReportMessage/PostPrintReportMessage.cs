﻿using System;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterDatabase.StoredProcedures.PrintReportMessage
{
    public class PostPrintReportMessage : StoredProcedureBase
    {
        public PostPrintReportMessage(Guid messageId, Guid? correlationId, ReportTypeEnum reportType, string templateId, string printerId, int numberOfCopy, bool? hasReprintFlag)
        {
            Parameters.Add("@messageId", messageId);
            Parameters.Add("@correlationId", correlationId);
            Parameters.Add("@reportType", reportType.ToString());
            Parameters.Add("@templateId", templateId);
            Parameters.Add("@printerId", printerId);
            Parameters.Add("@numberOfCopy", numberOfCopy);
            Parameters.Add("@hasReprintFlag", hasReprintFlag);
        }
    }
}