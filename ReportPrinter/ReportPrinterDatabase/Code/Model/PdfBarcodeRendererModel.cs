﻿using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfBarcodeRendererModel : PdfRendererBaseModel
    {
        public BarcodeFormat? BarcodeFormat { get; set; }
        public bool ShowBarcodeText { get; set; }
        public Guid SqlTemplateConfigSqlConfigId { get; set; }
        public string SqlTemplateId { get; set; }
        public string SqlId { get; set; }
        public string SqlResColumn { get; set; }
    }
}