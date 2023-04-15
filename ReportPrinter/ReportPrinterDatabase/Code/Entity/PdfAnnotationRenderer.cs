using System;
using System.Collections.Generic;

#nullable disable

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfAnnotationRenderer
    {
        public Guid PdfAnnotationRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public byte AnnotationRendererType { get; set; }
        public string Title { get; set; }
        public byte? Icon { get; set; }
        public string Content { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
