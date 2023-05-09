using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterDatabase.Code.Helper
{
    public class PdfRendererHelper<T> where T : PdfRendererBaseModel
    {
        public static T CreatePdfRenderer(PdfRendererBaseModel rendererBase)
        {
            var type = typeof(T);
            var model = (T)Activator.CreateInstance(type);
            
            model.Id = rendererBase.Id;
            model.RendererType = rendererBase.RendererType;
            model.Margin = rendererBase.Margin;
            model.Padding = rendererBase.Padding;
            model.HorizontalAlignment = rendererBase.HorizontalAlignment;
            model.VerticalAlignment = rendererBase.VerticalAlignment;
            model.Position = rendererBase.Position;
            model.Left = rendererBase.Left;
            model.Right = rendererBase.Right;
            model.Top = rendererBase.Top;
            model.Bottom = rendererBase.Bottom;
            model.FontSize = rendererBase.FontSize;
            model.FontFamily = rendererBase.FontFamily;
            model.FontStyle = rendererBase.FontStyle;
            model.Opacity = rendererBase.Opacity;
            model.BrushColor = rendererBase.BrushColor;
            model.BackgroundColor = rendererBase.BackgroundColor;
            model.Row = rendererBase.Row;
            model.Column = rendererBase.Column;
            model.RowSpan = rendererBase.RowSpan;
            model.ColumnSpan = rendererBase.ColumnSpan;

            return model;
        }
    }
}