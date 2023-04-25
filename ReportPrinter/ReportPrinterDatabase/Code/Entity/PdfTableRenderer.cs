#nullable disable

using System;
using System.Collections.Generic;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class PdfTableRenderer
    {
        public PdfTableRenderer()
        {
            InverseSubPdfTableRenderer = new HashSet<PdfTableRenderer>();
        }

        public Guid PdfTableRendererId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public double? BoardThickness { get; set; }
        public double? LineSpace { get; set; }
        public byte? TitleHorizontalAlignment { get; set; }
        public bool? HideTitle { get; set; }
        public double? Space { get; set; }
        public byte? TitleColor { get; set; }
        public double? TitleColorOpacity { get; set; }
        public Guid SqlTemplateConfigSqlConfigId { get; set; }
        public string SqlVariable { get; set; }
        public Guid? SubPdfTableRendererId { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
        public virtual SqlTemplateConfigSqlConfig SqlTemplateConfigSqlConfig { get; set; }
        public virtual PdfTableRenderer SubPdfTableRenderer { get; set; }
        public virtual ICollection<PdfTableRenderer> InverseSubPdfTableRenderer { get; set; }
    }
}
