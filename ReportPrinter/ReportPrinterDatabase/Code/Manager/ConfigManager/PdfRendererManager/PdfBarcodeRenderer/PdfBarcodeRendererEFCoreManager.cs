using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;
using ZXing;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer
{
    public class PdfBarcodeRendererEFCoreManager : IPdfBarcodeRendererManager
    {
        public async Task Post(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfBarcodeRenderers.Add(new Entity.PdfBarcodeRenderer());
                pdfRendererBase = CreateEntity(barcodeRenderer, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf barcode renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfBarcodeRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfBarcodeRenderers
                    .Include(x => x.PdfRendererBase)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF barcode renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF barcode renderer: {pdfRendererBaseId}", procName);

                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF barcode renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PutPdfBarcodeRenderer(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutPdfBarcodeRenderer)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfBarcodeRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == barcodeRenderer.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(barcodeRenderer, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf barcode renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        private PdfBarcodeRendererModel CreateDataModel(Entity.PdfBarcodeRenderer entity)
        {
            var model = new PdfBarcodeRendererModel
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
                ShowBarcodeText = entity.ShowBarcodeText,
                SqlTemplateId = entity.SqlTemplateId,
                SqlId = entity.SqlId,
                SqlResColumn = entity.SqlResColumn,
            };

            if (entity.PdfRendererBase.HorizontalAlignment.HasValue)
                model.HorizontalAlignment = (HorizontalAlignment)entity.PdfRendererBase.HorizontalAlignment.Value;

            if (entity.PdfRendererBase.VerticalAlignment.HasValue)
                model.VerticalAlignment = (VerticalAlignment)entity.PdfRendererBase.VerticalAlignment.Value;

            if (entity.PdfRendererBase.Position.HasValue)
                model.Position = (Position)entity.PdfRendererBase.Position.Value;
            
            if (entity.PdfRendererBase.FontStyle.HasValue)
                model.FontStyle = (XFontStyle)entity.PdfRendererBase.FontStyle.Value;

            if (entity.PdfRendererBase.BrushColor.HasValue)
                model.BrushColor = (XKnownColor)entity.PdfRendererBase.BrushColor.Value;

            if (entity.PdfRendererBase.BackgroundColor.HasValue)
                model.BackgroundColor = (XKnownColor)entity.PdfRendererBase.BackgroundColor.Value;

            if(entity.BarcodeFormat.HasValue)
                model.BarcodeFormat = (BarcodeFormat)entity.BarcodeFormat.Value;
            
            return model;
        }

        private PdfRendererBase CreateEntity(PdfBarcodeRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfBarcodeRenderer = pdfRendererBase.PdfBarcodeRenderers.Single();
            
            pdfBarcodeRenderer.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfBarcodeRenderer.BarcodeFormat = model.BarcodeFormat.HasValue ? (int?)model.BarcodeFormat.Value : null;
            pdfBarcodeRenderer.ShowBarcodeText = model.ShowBarcodeText;
            pdfBarcodeRenderer.SqlTemplateId = model.SqlTemplateId;
            pdfBarcodeRenderer.SqlId = model.SqlId;
            pdfBarcodeRenderer.SqlResColumn = model.SqlResColumn;
            
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

            return pdfRendererBase;
        }

        #endregion
    }
}