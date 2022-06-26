using System.Xml.Serialization;

namespace ReportPrinterLibrary.Config.Configuration
{
    public class DatabaseConfig
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}