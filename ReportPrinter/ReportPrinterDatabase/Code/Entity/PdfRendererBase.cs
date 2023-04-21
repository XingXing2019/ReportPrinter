using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfRendererBase
    {
        public PdfRendererBase()
        {
            PdfAnnotationRenderers = new HashSet<PdfAnnotationRenderer>();
            PdfBarcodeRenderers = new HashSet<PdfBarcodeRenderer>();
            PdfImageRenderers = new HashSet<PdfImageRenderer>();
            PdfPageNumberRenderers = new HashSet<PdfPageNumberRenderer>();
            PdfReprintMarkRenderers = new HashSet<PdfReprintMarkRenderer>();
            PdfTextRenderers = new HashSet<PdfTextRenderer>();
            PdfWaterMarkRenderers = new HashSet<PdfWaterMarkRenderer>();
        }

        public Guid PdfRendererBaseId { get; set; }
        public string Id { get; set; }
        public byte RendererType { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public byte? HorizontalAlignment { get; set; }
        public byte? VerticalAlignment { get; set; }
        public byte? Position { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
        public double? Bottom { get; set; }
        public double? FontSize { get; set; }
        public string FontFamily { get; set; }
        public byte? FontStyle { get; set; }
        public double? Opacity { get; set; }
        public byte? BrushColor { get; set; }
        public byte? BackgroundColor { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? RowSpan { get; set; }
        public int? ColumnSpan { get; set; }

        public virtual ICollection<PdfAnnotationRenderer> PdfAnnotationRenderers { get; set; }
        public virtual ICollection<PdfBarcodeRenderer> PdfBarcodeRenderers { get; set; }
        public virtual ICollection<PdfImageRenderer> PdfImageRenderers { get; set; }
        public virtual ICollection<PdfPageNumberRenderer> PdfPageNumberRenderers { get; set; }
        public virtual ICollection<PdfReprintMarkRenderer> PdfReprintMarkRenderers { get; set; }
        public virtual ICollection<PdfTextRenderer> PdfTextRenderers { get; set; }
        public virtual ICollection<PdfWaterMarkRenderer> PdfWaterMarkRenderers { get; set; }
    }
}
