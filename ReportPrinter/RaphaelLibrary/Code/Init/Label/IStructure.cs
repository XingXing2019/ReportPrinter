using RaphaelLibrary.Code.Render.Label.Manager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init.Label
{
    public interface IStructure
    {
        public IStructure Clone();
        public bool ReadFile(string filePath);
        public bool TryCreateLabelStructure(IPrintReport message);
    }
}