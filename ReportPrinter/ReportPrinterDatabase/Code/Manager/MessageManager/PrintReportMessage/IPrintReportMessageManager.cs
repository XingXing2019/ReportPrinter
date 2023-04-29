using System;
using System.Threading.Tasks;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.RabbitMQ.Message;

namespace ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage
{
    public interface IPrintReportMessageManager<T> : IMessageManager<T>
    {
        Task PatchStatus(Guid messageId, MessageStatus status);
    }
}