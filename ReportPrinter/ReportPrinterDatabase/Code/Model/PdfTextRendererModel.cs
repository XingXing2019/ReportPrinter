using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfTextRendererModel : PdfRendererBaseModel
    {
        public TextRendererType TextRendererType { get; set; }
        public string Content { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
        public string Mask { get; set; }
        public string Title { get; set; }
    }
}