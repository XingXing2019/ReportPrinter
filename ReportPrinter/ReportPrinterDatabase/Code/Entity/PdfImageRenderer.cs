using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfImageRenderer
    {
        public Guid PdfImageRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public byte SourceType { get; set; }
        public string ImageSource { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
