using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public class PdfAnnotationRendererEFCoreManager : PdfRendererManagerBase<PdfAnnotationRendererModel, Entity.PdfAnnotationRenderer>
    {
        public override async Task Post(PdfAnnotationRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfAnnotationRenderers.Add(new Entity.PdfAnnotationRenderer());
                pdfRendererBase = CreateEntity(model, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf annotation renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF annotation renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfAnnotationRendererModel> Get(Guid pdfRendererBaseId)
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

        public override async Task Put(PdfAnnotationRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutPdfBarcodeRenderer)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfAnnotationRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF annotation renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(model, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf annotation renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF annotation renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfAnnotationRendererModel CreateDataModel(Entity.PdfAnnotationRenderer entity)
        {
            var model = CreateDataModel(entity.PdfRendererBase);

            model.AnnotationRendererType = (AnnotationRendererType)entity.AnnotationRendererType;
            model.Title = entity.Title;
            model.Content = entity.Content;
            model.SqlTemplateId = entity.SqlTemplateId;
            model.SqlId = entity.SqlId;
            model.SqlResColumn = entity.SqlResColumn;

            if (entity.Icon.HasValue)
                model.Icon = (PdfTextAnnotationIcon)entity.Icon.Value;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfAnnotationRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfAnnotationRenderer = pdfRendererBase.PdfAnnotationRenderers.Single();

            pdfAnnotationRenderer.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfAnnotationRenderer.AnnotationRendererType = (byte)model.AnnotationRendererType;
            pdfAnnotationRenderer.Title = model.Title;
            pdfAnnotationRenderer.Icon = model.Icon.HasValue ? (byte?)model.Icon : null;
            pdfAnnotationRenderer.Content = model.Content;
            pdfAnnotationRenderer.SqlTemplateId = model.SqlTemplateId;
            pdfAnnotationRenderer.SqlId = model.SqlId;
            pdfAnnotationRenderer.SqlResColumn = model.SqlResColumn;
            
            AssignEntity(model, pdfRendererBase);
            return pdfRendererBase;
        }

        #endregion
    }
}