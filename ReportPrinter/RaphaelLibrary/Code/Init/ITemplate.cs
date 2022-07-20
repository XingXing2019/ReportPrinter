using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init
{
    public interface ITemplate : IXmlReader
    {
        public ITemplate Clone();
        public bool TryCreateReport(IPrintReport message);
    }
}