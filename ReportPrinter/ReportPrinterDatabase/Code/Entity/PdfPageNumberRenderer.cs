#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfPageNumberRenderer
    {
        public Guid PdfPageNumberRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public int? StartPage { get; set; }
        public int? EndPage { get; set; }
        public byte? PageNumberLocation { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
