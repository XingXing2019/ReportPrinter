using System;
using System.Threading.Tasks;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage
{
    public interface IPrintReportMessageManager<T> : IMessageManager<T>
    {
        public Task PatchStatus(Guid messageId, MessageStatus status);
    }
}