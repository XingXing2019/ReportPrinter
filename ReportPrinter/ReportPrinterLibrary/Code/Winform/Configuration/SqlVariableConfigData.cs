using System.ComponentModel;

namespace ReportPrinterLibrary.Code.Winform.Configuration
{
    public class SqlVariableConfigData
    {
        [DisplayName(" ")]
        public bool IsSelected { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
    }
}