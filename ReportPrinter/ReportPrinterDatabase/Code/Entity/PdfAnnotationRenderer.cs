#nullable disable

using System;

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
        public Guid? SqlTemplateConfigSqlConfigId { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
        public virtual SqlTemplateConfigSqlConfig SqlTemplateConfigSqlConfig { get; set; }
    }
}
