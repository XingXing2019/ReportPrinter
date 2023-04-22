using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfWaterMarkRenderer
{
    public class PdfWaterMarkRendererEFCoreManager : PdfRendererManagerBase<PdfWaterMarkRendererModel, Entity.PdfWaterMarkRenderer>
    {
        public override async Task Post(PdfWaterMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfWaterMarkRenderers.Add(new Entity.PdfWaterMarkRenderer());

                pdfRendererBase = CreateEntity(model, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf water mark renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF water mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfWaterMarkRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfWaterMarkRenderers
                    .Include(x => x.PdfRendererBase)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF water mark renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF water mark renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF water mark renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfWaterMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfWaterMarkRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF water mark renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(model, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf water mark renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF water mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfWaterMarkRendererModel CreateDataModel(Entity.PdfWaterMarkRenderer entity)
        {
            var model = CreateDataModel(entity.PdfRendererBase);

            model.WaterMarkType = (WaterMarkRendererType)entity.WaterMarkType;
            model.Content = entity.Content;
            model.Location = (Location?)entity.Location;
            model.SqlTemplateId = entity.SqlTemplateId;
            model.SqlId = entity.SqlId;
            model.SqlResColumn = entity.SqlResColumn;
            model.StartPage = entity.StartPage;
            model.EndPage = entity.EndPage;
            model.Rotate = entity.Rotate;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfWaterMarkRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfImageRender = pdfRendererBase.PdfWaterMarkRenderers.Single();

            pdfImageRender.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfImageRender.WaterMarkType = (byte)model.WaterMarkType;
            pdfImageRender.Content = model.Content;
            pdfImageRender.Location = (byte?)model.Location;
            pdfImageRender.SqlTemplateId = model.SqlTemplateId;
            pdfImageRender.SqlId = model.SqlId;
            pdfImageRender.SqlResColumn = model.SqlResColumn;
            pdfImageRender.StartPage = model.StartPage;
            pdfImageRender.EndPage = model.EndPage;
            pdfImageRender.Rotate = model.Rotate;

            AssignEntity(model, pdfRendererBase);
            return pdfRendererBase;
        }

        #endregion
    }
}