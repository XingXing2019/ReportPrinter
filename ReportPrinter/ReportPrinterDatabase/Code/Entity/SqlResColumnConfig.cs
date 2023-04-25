#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlResColumnConfig
    {
        public Guid SqlResColumnConfigId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public double? WidthRatio { get; set; }
        public byte? Position { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
