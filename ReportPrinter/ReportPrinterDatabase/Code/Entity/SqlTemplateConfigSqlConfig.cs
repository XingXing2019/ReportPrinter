#nullable disable

using System;
using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlTemplateConfigSqlConfig
    {
        public SqlTemplateConfigSqlConfig()
        {
            PdfAnnotationRenderers = new HashSet<PdfAnnotationRenderer>();
            PdfBarcodeRenderers = new HashSet<PdfBarcodeRenderer>();
            PdfTextRenderers = new HashSet<PdfTextRenderer>();
            PdfWaterMarkRenderers = new HashSet<PdfWaterMarkRenderer>();
        }

        public Guid SqlTemplateConfigSqlConfigId { get; set; }
        public Guid SqlTemplateConfigId { get; set; }
        public Guid SqlConfigId { get; set; }

        public virtual SqlConfig SqlConfig { get; set; }
        public virtual SqlTemplateConfig SqlTemplateConfig { get; set; }
        public virtual ICollection<PdfAnnotationRenderer> PdfAnnotationRenderers { get; set; }
        public virtual ICollection<PdfBarcodeRenderer> PdfBarcodeRenderers { get; set; }
        public virtual ICollection<PdfTextRenderer> PdfTextRenderers { get; set; }
        public virtual ICollection<PdfWaterMarkRenderer> PdfWaterMarkRenderers { get; set; }
    }
}
