#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfTextRenderer
    {
        public Guid PdfTextRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public byte TextRendererType { get; set; }
        public string Content { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
        public string Mask { get; set; }
        public string Title { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
