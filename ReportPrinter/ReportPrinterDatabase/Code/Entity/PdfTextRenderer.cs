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
        public Guid? SqlTemplateConfigSqlConfigId { get; set; }
        public string Mask { get; set; }
        public string Title { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
        public virtual SqlTemplateConfigSqlConfig SqlTemplateConfigSqlConfig { get; set; }
    }
}
