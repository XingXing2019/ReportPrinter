using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using PdfSharp.Pdf.Annotations;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;
using ZXing.Rendering;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public class PdfAnnotationRendererEFCoreManager : IPdfAnnotationRendererManager
    {
        public async Task Post(PdfAnnotationRendererModel annotationRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfAnnotationRenderers.Add(new Entity.PdfAnnotationRenderer());
                pdfRendererBase = CreateEntity(annotationRenderer, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf annotation renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF annotation renderer: {annotationRenderer.RendererBase.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfAnnotationRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfAnnotationRenderers
                    .Include(x => x.PdfRendererBase)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF annotation renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF annotation renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF annotation renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PutPdfAnnotationRenderer(PdfAnnotationRendererModel annotationRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutPdfBarcodeRenderer)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfAnnotationRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == annotationRenderer.RendererBase.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF annotation renderer: {annotationRenderer.RendererBase.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(annotationRenderer, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf annotation renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF annotation renderer: {annotationRenderer.RendererBase.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        private PdfAnnotationRendererModel CreateDataModel(Entity.PdfAnnotationRenderer entity)
        {
            var model = new PdfAnnotationRendererModel
            {
                RendererBase = new PdfRendererBaseModel
                {
                    PdfRendererBaseId = entity.PdfRendererBaseId,
                    Id = entity.PdfRendererBase.Id,
                    RendererType = (PdfRendererType)entity.PdfRendererBase.RendererType,
                    Margin = entity.PdfRendererBase.Margin,
                    Padding = entity.PdfRendererBase.Padding,
                    Left = entity.PdfRendererBase.Left,
                    Right = entity.PdfRendererBase.Right,
                    Top = entity.PdfRendererBase.Top,
                    Bottom = entity.PdfRendererBase.Bottom,
                    FontSize = entity.PdfRendererBase.FontSize,
                    FontFamily = entity.PdfRendererBase.FontFamily,
                    Opacity = entity.PdfRendererBase.Opacity,
                    Row = entity.PdfRendererBase.Row,
                    Column = entity.PdfRendererBase.Column,
                    RowSpan = entity.PdfRendererBase.RowSpan,
                    ColumnSpan = entity.PdfRendererBase.ColumnSpan,
                },
                AnnotationRendererType = (AnnotationRendererType)entity.AnnotationRendererType,
                Title = entity.Title,
                Content = entity.Content,
                SqlTemplateId = entity.SqlTemplateId,
                SqlId = entity.SqlId,
                SqlResColumn = entity.SqlResColumn,
            };

            if (entity.PdfRendererBase.HorizontalAlignment.HasValue)
                model.RendererBase.HorizontalAlignment = (HorizontalAlignment)entity.PdfRendererBase.HorizontalAlignment.Value;

            if (entity.PdfRendererBase.VerticalAlignment.HasValue)
                model.RendererBase.VerticalAlignment = (VerticalAlignment)entity.PdfRendererBase.VerticalAlignment.Value;

            if (entity.PdfRendererBase.Position.HasValue)
                model.RendererBase.Position = (Position)entity.PdfRendererBase.Position.Value;

            if (entity.PdfRendererBase.FontStyle.HasValue)
                model.RendererBase.FontStyle = (XFontStyle)entity.PdfRendererBase.FontStyle.Value;

            if (entity.PdfRendererBase.BrushColor.HasValue)
                model.RendererBase.BrushColor = (XKnownColor)entity.PdfRendererBase.BrushColor.Value;

            if (entity.PdfRendererBase.BackgroundColor.HasValue)
                model.RendererBase.BackgroundColor = (XKnownColor)entity.PdfRendererBase.BackgroundColor.Value;

            if (entity.Icon.HasValue)
                model.Icon = (PdfTextAnnotationIcon)entity.Icon.Value;

            return model;
        }

        private PdfRendererBase CreateEntity(PdfAnnotationRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfAnnotationRenderer = pdfRendererBase.PdfAnnotationRenderers.Single();

            pdfAnnotationRenderer.PdfRendererBaseId = model.RendererBase.PdfRendererBaseId;
            pdfAnnotationRenderer.AnnotationRendererType = (byte)model.AnnotationRendererType;
            pdfAnnotationRenderer.Title = model.Title;
            pdfAnnotationRenderer.Icon = model.Icon.HasValue ? (byte?)model.Icon : null;
            pdfAnnotationRenderer.Content = model.Content;
            pdfAnnotationRenderer.SqlTemplateId = model.SqlTemplateId;
            pdfAnnotationRenderer.SqlId = model.SqlId;
            pdfAnnotationRenderer.SqlResColumn = model.SqlResColumn;

            pdfRendererBase.PdfRendererBaseId = model.RendererBase.PdfRendererBaseId;
            pdfRendererBase.Id = model.RendererBase.Id;
            pdfRendererBase.RendererType = (byte)model.RendererBase.RendererType;
            pdfRendererBase.Margin = model.RendererBase.Margin;
            pdfRendererBase.Padding = model.RendererBase.Padding;
            pdfRendererBase.HorizontalAlignment = model.RendererBase.HorizontalAlignment.HasValue ? (byte?)model.RendererBase.HorizontalAlignment.Value : null;
            pdfRendererBase.VerticalAlignment = model.RendererBase.VerticalAlignment.HasValue ? (byte?)model.RendererBase.VerticalAlignment.Value : null;
            pdfRendererBase.Position = model.RendererBase.Position.HasValue ? (byte?)model.RendererBase.Position.Value : null;
            pdfRendererBase.Left = model.RendererBase.Left;
            pdfRendererBase.Right = model.RendererBase.Right;
            pdfRendererBase.Top = model.RendererBase.Top;
            pdfRendererBase.Bottom = model.RendererBase.Bottom;
            pdfRendererBase.FontSize = model.RendererBase.FontSize;
            pdfRendererBase.FontFamily = model.RendererBase.FontFamily;
            pdfRendererBase.FontStyle = model.RendererBase.FontStyle.HasValue ? (byte?)model.RendererBase.FontStyle.Value : null;
            pdfRendererBase.Opacity = model.RendererBase.Opacity;
            pdfRendererBase.BrushColor = model.RendererBase.BrushColor.HasValue ? (byte?)model.RendererBase.BrushColor.Value : null;
            pdfRendererBase.BackgroundColor = model.RendererBase.BackgroundColor.HasValue ? (byte?)model.RendererBase.BackgroundColor.Value : null;
            pdfRendererBase.Row = model.RendererBase.Row;
            pdfRendererBase.Column = model.RendererBase.Column;
            pdfRendererBase.RowSpan = model.RendererBase.RowSpan;
            pdfRendererBase.ColumnSpan = model.RendererBase.ColumnSpan;

            return pdfRendererBase;
        }

        #endregion
    }
}