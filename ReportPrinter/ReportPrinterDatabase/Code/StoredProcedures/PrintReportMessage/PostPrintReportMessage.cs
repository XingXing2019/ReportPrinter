using System;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage
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