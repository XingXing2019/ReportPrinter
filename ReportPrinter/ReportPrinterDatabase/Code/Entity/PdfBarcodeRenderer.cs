using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfBarcodeRenderer
    {
        public Guid PdfBarcodeRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public byte? BarcodeFormat { get; set; }
        public bool ShowBarcodeText { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
