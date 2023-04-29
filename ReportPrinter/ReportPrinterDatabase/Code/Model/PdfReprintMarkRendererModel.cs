using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfReprintMarkRendererModel : PdfRendererBaseModel
    {
        public string Text { get; set; }
        public double? BoardThickness { get; set; }
        public Location? Location { get; set; }
    }
}