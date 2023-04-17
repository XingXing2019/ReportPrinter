using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfRendererBaseModel
    {
        public Guid PdfRendererBaseId { get; set; }
        public string Id { get; set; }
        public PdfRendererType RendererType { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}