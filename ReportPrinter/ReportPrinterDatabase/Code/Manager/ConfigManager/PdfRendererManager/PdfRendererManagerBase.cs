using System;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager
{
    public class PdfRendererManagerBase<T> where T : PdfRendererBaseModel
    {
        protected void AssignRendererBaseModelProperties(PdfRendererBaseModel from, PdfBarcodeRendererModel to)
        {
            to.PdfRendererBaseId = from.PdfRendererBaseId;
            to.Id = from.Id;
            to.RendererType = from.RendererType;
            to.Margin = from.Margin;
            to.Padding = from.Padding;
            to.HorizontalAlignment = from.HorizontalAlignment;
            to.VerticalAlignment = from.VerticalAlignment;
            to.Position = from.Position;
            to.Left = from.Left;
            to.Right = from.Right;
            to.Top = from.Top;
            to.Bottom = from.Bottom;
            to.FontSize = from.FontSize;
            to.FontFamily = from.FontFamily;
            to.FontStyle = from.FontStyle;
            to.Opacity = from.Opacity;
            to.BrushColor = from.BrushColor;
            to.BackgroundColor = from.BackgroundColor;
            to.Row = from.Row;
            to.Column = from.Column;
            to.RowSpan = from.RowSpan;
            to.ColumnSpan = from.ColumnSpan;
        }

        protected T CreateDataModel(PdfRendererBase entity)
        {
            var type = typeof(T);
            var model = (T)Activator.CreateInstance(type);

            model.PdfRendererBaseId = entity.PdfRendererBaseId;
            model.Id = entity.Id;
            model.RendererType = (PdfRendererType)entity.RendererType;
            model.Margin = entity.Margin;
            model.Padding = entity.Padding;
            model.Left = entity.Left;
            model.Right = entity.Right;
            model.Top = entity.Top;
            model.Bottom = entity.Bottom;
            model.FontSize = entity.FontSize;
            model.FontFamily = entity.FontFamily;
            model.Opacity = entity.Opacity;
            model.Row = entity.Row;
            model.Column = entity.Column;
            model.RowSpan = entity.RowSpan;
            model.ColumnSpan = entity.ColumnSpan;

            if (entity.HorizontalAlignment.HasValue)
                model.HorizontalAlignment = (HorizontalAlignment)entity.HorizontalAlignment.Value;

            if (entity.VerticalAlignment.HasValue)
                model.VerticalAlignment = (VerticalAlignment)entity.VerticalAlignment.Value;

            if (entity.Position.HasValue)
                model.Position = (Position)entity.Position.Value;

            if (entity.FontStyle.HasValue)
                model.FontStyle = (XFontStyle)entity.FontStyle.Value;

            if (entity.BrushColor.HasValue)
                model.BrushColor = (XKnownColor)entity.BrushColor.Value;

            if (entity.BackgroundColor.HasValue)
                model.BackgroundColor = (XKnownColor)entity.BackgroundColor.Value;

            return model;
        }

        protected void AssignEntity(T model, PdfRendererBase pdfRendererBase)
        {
            pdfRendererBase.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfRendererBase.Id = model.Id;
            pdfRendererBase.RendererType = (byte)model.RendererType;
            pdfRendererBase.Margin = model.Margin;
            pdfRendererBase.Padding = model.Padding;
            pdfRendererBase.HorizontalAlignment = model.HorizontalAlignment.HasValue ? (byte?)model.HorizontalAlignment.Value : null;
            pdfRendererBase.VerticalAlignment = model.VerticalAlignment.HasValue ? (byte?)model.VerticalAlignment.Value : null;
            pdfRendererBase.Position = model.Position.HasValue ? (byte?)model.Position.Value : null;
            pdfRendererBase.Left = model.Left;
            pdfRendererBase.Right = model.Right;
            pdfRendererBase.Top = model.Top;
            pdfRendererBase.Bottom = model.Bottom;
            pdfRendererBase.FontSize = model.FontSize;
            pdfRendererBase.FontFamily = model.FontFamily;
            pdfRendererBase.FontStyle = model.FontStyle.HasValue ? (byte?)model.FontStyle.Value : null;
            pdfRendererBase.Opacity = model.Opacity;
            pdfRendererBase.BrushColor = model.BrushColor.HasValue ? (byte?)model.BrushColor.Value : null;
            pdfRendererBase.BackgroundColor = model.BackgroundColor.HasValue ? (byte?)model.BackgroundColor.Value : null;
            pdfRendererBase.Row = model.Row;
            pdfRendererBase.Column = model.Column;
            pdfRendererBase.RowSpan = model.RowSpan;
            pdfRendererBase.ColumnSpan = model.ColumnSpan;
        }
    }
}