using System.Xml;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelTemplate : ITemplate
    {
        public bool ReadXml(XmlNode node)
        {
            throw new System.NotImplementedException();
        }

        public ITemplate Clone()
        {
            throw new System.NotImplementedException();
        }

        public bool TryCreateReport(IPrintReport message)
        {
            throw new System.NotImplementedException();
        }
    }
}