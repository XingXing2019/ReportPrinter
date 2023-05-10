using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterLibrary.Code.Winform.Configuration.SQL
{
    public class SqlResColumnData
    {
        public bool IsSelected { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public double? WidthRatio { get; set; }

        public Position? Position { get; set; }

        public double? Left { get; set; }

        public double? Right { get; set; }
    }
}