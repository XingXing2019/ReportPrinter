using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager
{
    public class PdfRendererBaseEFCoreManager : IPdfRendererBaseManager
    {
        public async Task Post(PdfRendererBaseModel pdfRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase
                {
                    PdfRendererBaseId = pdfRenderer.PdfRendererBaseId,
                    Id = pdfRenderer.Id,
                    RendererType = (byte)pdfRenderer.RendererType,
                    Margin = pdfRenderer.Margin,
                    Padding = pdfRenderer.Padding,
                    HorizontalAlignment = pdfRenderer.HorizontalAlignment.HasValue ? (byte?)pdfRenderer.HorizontalAlignment.Value : null,
                    VerticalAlignment = pdfRenderer.VerticalAlignment.HasValue ? (byte?)pdfRenderer.VerticalAlignment.Value : null,
                    Position = pdfRenderer.Position.HasValue ? (byte?)pdfRenderer.Position.Value : null,
                    Left = pdfRenderer.Left,
                    Right = pdfRenderer.Right,
                    Top = pdfRenderer.Top,
                    Bottom = pdfRenderer.Bottom,
                    FontSize = pdfRenderer.FontSize,
                    FontFamily = pdfRenderer.FontFamily,
                    FontStyle = pdfRenderer.FontStyle.HasValue ? (byte?)pdfRenderer.FontStyle.Value : null,
                    Opacity = pdfRenderer.Opacity,
                    BrushColor = pdfRenderer.BrushColor.HasValue ? (byte?)pdfRenderer.BrushColor.Value : null,
                    BackgroundColor = pdfRenderer.BackgroundColor.HasValue ? (byte?)pdfRenderer.BackgroundColor.Value : null,
                    Row = pdfRenderer.Row,
                    Column = pdfRenderer.Column,
                    RowSpan = pdfRenderer.RowSpan,
                    ColumnSpan = pdfRenderer.ColumnSpan
                };

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record PDF renderer: {pdfRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF renderer: {pdfRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfRendererBaseModel> Get(Guid pdfRendererId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases.FindAsync(pdfRendererId);

                if (entity == null)
                {
                    Logger.Debug($"PDF renderer: {pdfRendererId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF renderer: {pdfRendererId}", procName);

                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF renderer: {pdfRendererId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IList<PdfRendererBaseModel>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.PdfRendererBases.ToListAsync();

                Logger.Debug($"Retrieve all PDF renderers", procName);
                return entities.Select(CreateDataModel).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(Guid pdfRendererId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases.FindAsync(pdfRendererId);

                if (entity == null)
                {
                    Logger.Debug($"PDF renderer: {pdfRendererId} does not exist", procName);
                }
                else
                {
                    context.PdfRendererBases.Remove(entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete PDF renderer: {pdfRendererId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderer: {pdfRendererId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(List<Guid> pdfRendererIds)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.PdfRendererBases.Where(x => pdfRendererIds.Contains(x.PdfRendererBaseId)).ToListAsync();

                if (entities.Count == 0)
                {
                    Logger.Debug($"PDF renderers does not exist", procName);
                }
                else
                {
                    context.PdfRendererBases.RemoveRange(entities);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete PDF renderers, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                await using var context = new ReportPrinterContext();
                context.PdfRendererBases.RemoveRange(context.PdfRendererBases);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Delete PDF renderers, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<PdfRendererBaseModel>> GetAllByRendererType(PdfRendererType rendererType)
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAllByRendererType)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.PdfRendererBases.Where(x => x.RendererType == (byte)rendererType).ToListAsync();

                Logger.Debug($"Retrieve all PDF renderers by with type: {rendererType}", procName);
                return entities.Select(CreateDataModel).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderer with type: {rendererType}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<PdfRendererBaseModel>> GetAlByRendererIdPrefix(string rendererIdPrefix)
        {
            var procName = $"{this.GetType()}.{nameof(GetAlByRendererIdPrefix)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entities = await context.PdfRendererBases.Where(x => x.Id.StartsWith(rendererIdPrefix)).ToListAsync();

                Logger.Debug($"Retrieve all PDF renderers by renderer id prefix: {rendererIdPrefix}", procName);
                return entities.Select(CreateDataModel).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderers by renderer id prefix: {rendererIdPrefix}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region

        private PdfRendererBaseModel CreateDataModel(PdfRendererBase entity)
        {
            return new PdfRendererBaseModel
            {
                PdfRendererBaseId = entity.PdfRendererBaseId,
                Id = entity.Id,
                RendererType = (PdfRendererType)entity.RendererType,
                Margin = entity.Margin,
                Padding = entity.Padding,
                HorizontalAlignment = entity.HorizontalAlignment.HasValue ? (HorizontalAlignment?)entity.HorizontalAlignment : null,
                VerticalAlignment = entity.VerticalAlignment.HasValue ? (VerticalAlignment?)entity.VerticalAlignment : null,
                Position = entity.Position.HasValue ? (Position?)entity.Position : null,
                Left = entity.Left,
                Right = entity.Right,
                Top = entity.Top,
                Bottom = entity.Bottom,
                FontSize = entity.FontSize,
                FontFamily = entity.FontFamily,
                FontStyle = entity.FontStyle.HasValue ? (XFontStyle?)entity.FontStyle : null,
                Opacity = entity.Opacity,
                BrushColor = entity.BrushColor.HasValue ? (XKnownColor?)entity.BrushColor : null,
                BackgroundColor = entity.BackgroundColor.HasValue ? (XKnownColor?)entity.BackgroundColor : null,
                Row = entity.Row,
                Column = entity.Column,
                RowSpan = entity.RowSpan,
                ColumnSpan = entity.ColumnSpan,
            };
        }

        #endregion
    }
}