namespace ReportPrinterDatabase.Code.Model
{
    public class PdfPageNumberRendererModel : PdfRendererBaseModel
    {
        public int? StartPage { get; set; }
        public int? EndPage { get; set; }
        public byte? PageNumberLocation { get; set; }
    }
}