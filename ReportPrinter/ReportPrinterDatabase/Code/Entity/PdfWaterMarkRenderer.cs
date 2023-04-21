using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfWaterMarkRenderer
    {
        public Guid PdfWaterMarkRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public byte WaterMarkType { get; set; }
        public string Content { get; set; }
        public byte? Location { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
        public int? StartPage { get; set; }
        public int? EndPage { get; set; }
        public double? Rotate { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
