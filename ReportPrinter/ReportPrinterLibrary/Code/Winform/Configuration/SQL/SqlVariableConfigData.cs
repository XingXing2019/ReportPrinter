using System.ComponentModel;
using System.Xml.Serialization;

namespace ReportPrinterLibrary.Code.Winform.Configuration.SQL
{
    [XmlRoot("Variable")]
    public class SqlVariableConfigData
    {
        [XmlIgnore]
        [DisplayName(" ")]
        public bool IsSelected { get; set; }

        [XmlAttribute("Name")]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}