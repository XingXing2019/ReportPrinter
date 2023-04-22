using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager
{
    public abstract class PdfRendererManagerBase<T, E> where T : PdfRendererBaseModel
    {
        protected readonly StoredProcedureExecutor Executor;

        protected PdfRendererManagerBase()
        {
            Executor = new StoredProcedureExecutor();
        }

        public abstract Task Post(T model);
        public abstract Task<T> Get(Guid pdfRendererBaseId);
        public abstract Task Put(T model);
        protected virtual T CreateDataModel(E entity) => default(T);
        protected virtual PdfRendererBase CreateEntity(T model, PdfRendererBase pdfRendererBase) => pdfRendererBase;
        
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

        protected List<StoredProcedureBase> CreatePostStoreProcedures(T model)
        {
            var spList = new List<StoredProcedureBase>
            {
                new PostPdfRendererBase(
                    model.PdfRendererBaseId,
                    model.Id,
                    (byte)model.RendererType,
                    model.Margin,
                    model.Padding,
                    (byte?)model.HorizontalAlignment,
                    (byte?)model.VerticalAlignment,
                    (byte?)model.Position,
                    model.Left,
                    model.Right,
                    model.Top,
                    model.Bottom,
                    model.FontSize,
                    model.FontFamily,
                    (byte?)model.FontStyle,
                    model.Opacity,
                    (byte?)model.BrushColor,
                    (byte?)model.BackgroundColor,
                    model.Row,
                    model.Column,
                    model.RowSpan,
                    model.ColumnSpan
                )
            };

            return spList;
        }

        protected List<StoredProcedureBase> CreatePutStoreProcedures(T model)
        {
            var spList = new List<StoredProcedureBase>
            {
                new PutPdfRendererBase(
                    model.PdfRendererBaseId,
                    model.Id,
                    (byte)model.RendererType,
                    model.Margin,
                    model.Padding,
                    (byte?)model.HorizontalAlignment,
                    (byte?)model.VerticalAlignment,
                    (byte?)model.Position,
                    model.Left,
                    model.Right,
                    model.Top,
                    model.Bottom,
                    model.FontSize,
                    model.FontFamily,
                    (byte?)model.FontStyle,
                    model.Opacity,
                    (byte?)model.BrushColor,
                    (byte?)model.BackgroundColor,
                    model.Row,
                    model.Column,
                    model.RowSpan,
                    model.ColumnSpan
                )
            };

            return spList;
        }
    }
}