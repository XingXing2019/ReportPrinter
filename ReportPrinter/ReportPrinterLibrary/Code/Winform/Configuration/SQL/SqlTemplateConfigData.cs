using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ReportPrinterLibrary.Code.Winform.Configuration.SQL
{
    [XmlRoot("SqlTemplate")]
    public class SqlTemplateConfigData
    {
        [XmlIgnore]
        [DisplayName(" ")]
        public bool IsSelected { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public Guid SqlTemplateConfigId { get; set; }

        [XmlAttribute("Id")]
        [DisplayName("Id")]
        public string Id { get; set; }

        [XmlElement("Sql")]
        [Browsable(false)]
        public List<SqlConfigData> SqlConfigs { get; set; }
    }
}