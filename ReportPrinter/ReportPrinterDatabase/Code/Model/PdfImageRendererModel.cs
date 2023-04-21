using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfImageRendererModel : PdfRendererBaseModel
    {
        public SourceType SourceType { get; set; }
        public string ImageSource { get; set; }
    }
}