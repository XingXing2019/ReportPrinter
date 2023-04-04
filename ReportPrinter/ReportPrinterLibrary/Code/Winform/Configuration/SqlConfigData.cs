using System.ComponentModel;

namespace ReportPrinterLibrary.Code.Winform.Configuration
{
    public class SqlConfigData
    {
        [DisplayName(" ")]
        public bool IsChecked { get; set; }

        [DisplayName("Id")]
        public string Id { get; set; }

        [DisplayName("Database Id")]
        public string DatabaseId { get; set; }
        
        [DisplayName("Query")]
        public string Query { get; set; }
    }
}