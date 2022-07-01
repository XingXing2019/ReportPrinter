using System.Xml;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Init.PDF
{
    public class PdfTemplate : IXmlReader
    {
        public string Id { get; private set; }

        public bool ReadXml(XmlNode node)
        {
            var procName = $"{this.GetType().Name}.{nameof(ReadXml)}";

            var id = node.Attributes?[XmlElementName.S_ID]?.Value;
            if (string.IsNullOrEmpty(id))
            {
                var missingXmlLog = Logger.GenerateMissingXmlLog(XmlElementName.S_ID, node);
                Logger.Error(missingXmlLog, procName);
                return false;
            }
            Id = id;

            return true;
        }

        public PdfTemplate Clone()
        {
            var cloned = this.MemberwiseClone() as PdfTemplate;


            return cloned;
        }
    }
}