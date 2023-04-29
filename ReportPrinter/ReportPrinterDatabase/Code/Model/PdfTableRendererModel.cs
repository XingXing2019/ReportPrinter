using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfTableRendererModel : PdfRendererBaseModel
    {
        public double? BoardThickness { get; set; }
        public double? LineSpace { get; set; }
        public HorizontalAlignment? TitleHorizontalAlignment { get; set; }
        public bool? HideTitle { get; set; }
        public double? Space { get; set; }
        public XKnownColor? TitleColor { get; set; }
        public double? TitleColorOpacity { get; set; }
        public Guid SqlTemplateConfigSqlConfigId { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public List<SqlResColumnModel> SqlResColumns { get; set; }
        public string SqlVariable { get; set; }
        public Guid? SubPdfTableRendererId { get; set; }
        public PdfTableRendererModel SubPdfTableRenderer { get; set; }
    }
}