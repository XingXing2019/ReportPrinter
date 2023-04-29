#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfReprintMarkRenderer
    {
        public Guid PdfReprintMarkRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public string Text { get; set; }
        public double? BoardThickness { get; set; }
        public byte? Location { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
