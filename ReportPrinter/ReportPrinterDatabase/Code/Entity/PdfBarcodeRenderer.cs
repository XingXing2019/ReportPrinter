#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfBarcodeRenderer
    {
        public Guid PdfBarcodeRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public int? BarcodeFormat { get; set; }
        public bool ShowBarcodeText { get; set; }
        public Guid SqlTemplateConfigSqlConfigId { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
        public virtual SqlTemplateConfigSqlConfig SqlTemplateConfigSqlConfig { get; set; }
    }
}
