using System.Text;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init.Label
{
    public interface IStructure
    {
        public IStructure Clone();
        public bool ReadFile(string filePath);
        public bool TryCreateLabelStructure(IPrintReport message, out StringBuilder labelStructure);
    }
}