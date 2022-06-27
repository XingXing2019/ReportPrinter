using System;
using MassTransit;

namespace ReportPrinterLibrary.RabbitMQ.Message
{
    public interface IMessage : CorrelatedBy<Guid>
    {
        public Guid MessageId { get; }
    }
}