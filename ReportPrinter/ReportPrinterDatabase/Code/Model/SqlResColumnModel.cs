using System;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class SqlResColumnModel
    {
        public Guid PdfRendererBaseId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public double? WidthRatio { get; set; }
        public Position? Position { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
    }
}