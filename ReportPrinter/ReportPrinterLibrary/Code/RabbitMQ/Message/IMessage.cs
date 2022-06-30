using System;

namespace ReportPrinterLibrary.RabbitMQ.Message
{
    public interface IMessage
    {
        public Guid MessageId { get; set; }
        public Guid? CorrelationId { get; set; }
    }
    
    public enum MessageStatus
    {
        Publish,
        Receive,
        Complete,
        Error
    }
}