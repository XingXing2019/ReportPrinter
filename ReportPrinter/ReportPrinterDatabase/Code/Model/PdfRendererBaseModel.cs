﻿using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterDatabase.Code.Model
{
    public class PdfRendererBaseModel
    {
        public Guid PdfRendererBaseId { get; set; }
        public string Id { get; set; }
        public PdfRendererType RendererType { get; set; }
        public string Margin { get; set; }
        public string Padding { get; set; }
        public HorizontalAlignment? HorizontalAlignment { get; set; }
        public VerticalAlignment? VerticalAlignment { get; set; }
        public Position? Position { get; set; }
        public double? Left { get; set; }
        public double? Right { get; set; }
        public double? Top { get; set; }
        public double? Bottom { get; set; }
        public double? FontSize { get; set; }
        public string FontFamily { get; set; }
        public XFontStyle? FontStyle { get; set; }
        public double? Opacity { get; set; }
        public XKnownColor? BrushColor { get; set; }
        public XKnownColor? BackgroundColor { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int? RowSpan { get; set; }
        public int? ColumnSpan { get; set; }
    }
}