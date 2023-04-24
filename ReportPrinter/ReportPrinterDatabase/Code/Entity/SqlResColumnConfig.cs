#nullable disable

using System;

namespace ReportPrinterDatabase.Code.Entity
{
    public partial class SqlResColumnConfig
    {
        public Guid SqlResColumnConfigId { get; set; }
        public Guid PdfRendererBaseId { get; set; }
        public string Name { get; set; }

        public virtual PdfRendererBase PdfRendererBase { get; set; }
    }
}
