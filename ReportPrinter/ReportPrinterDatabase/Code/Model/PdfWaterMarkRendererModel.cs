using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfWaterMarkRendererModel : PdfRendererBaseModel
    {
        public WaterMarkRendererType WaterMarkType { get; set; }
        public string Content { get; set; }
        public Location? Location { get; set; }
        public Guid? SqlTemplateConfigSqlConfigId { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
        public int? StartPage { get; set; }
        public int? EndPage { get; set; }
        public double? Rotate { get; set; }
    }
}