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
        }

        public Guid SqlTemplateConfigSqlConfigId { get; set; }
        public Guid SqlTemplateConfigId { get; set; }
        public Guid SqlConfigId { get; set; }

        public virtual SqlConfig SqlConfig { get; set; }
        public virtual SqlTemplateConfig SqlTemplateConfig { get; set; }
        public virtual ICollection<PdfAnnotationRenderer> PdfAnnotationRenderers { get; set; }
    }
}
