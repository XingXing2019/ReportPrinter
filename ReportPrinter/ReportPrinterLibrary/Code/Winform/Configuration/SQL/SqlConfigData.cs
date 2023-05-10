using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace ReportPrinterLibrary.Code.Winform.Configuration.SQL
{
    [XmlRoot("Sql")]
    public class SqlConfigData
    {
        [XmlIgnore]
        [DisplayName(" ")]
        public bool IsSelected { get; set; }

        [XmlIgnore]
        [DisplayName(" ")]
        [Browsable(false)]
        public Guid SqlConfigId { get; set; }

        [XmlAttribute("Id")]
        [DisplayName("Id")]
        public string Id { get; set; }

        [XmlAttribute("DatabaseId")]
        [DisplayName("Database Id")]
        public string DatabaseId { get; set; }

        [XmlElement("Query")]
        [Browsable(false)]
        public string Query { get; set; }

        [XmlElement("Variable")]
        [Browsable(false)]
        public List<SqlVariableConfigData> SqlVariableConfigs { get; set; }

        public SqlConfigData()
        {
            SqlVariableConfigs = new List<SqlVariableConfigData>();
        }
    }
}