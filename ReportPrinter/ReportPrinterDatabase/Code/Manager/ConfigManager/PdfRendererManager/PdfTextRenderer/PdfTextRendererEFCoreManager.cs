using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTextRenderer
{
    public class PdfTextRendererEFCoreManager : PdfRendererManagerBase<PdfTextRendererModel, Entity.PdfTextRenderer>
    {
        public override async Task Post(PdfTextRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfTextRenderers.Add(new Entity.PdfTextRenderer());

                pdfRendererBase = CreateEntity(model, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf text renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF text renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfTextRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfTextRenderers
                    .Include(x => x.PdfRendererBase)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF text renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF text renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF text renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfTextRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfTextRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF text renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(model, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf text renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF text renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfTextRendererModel CreateDataModel(Entity.PdfTextRenderer entity)
        {
            var model = CreateDataModel(entity.PdfRendererBase);

            model.TextRendererType = (TextRendererType)entity.TextRendererType;
            model.Content = entity.Content;
            model.SqlTemplateId = entity.SqlTemplateId;
            model.SqlId = entity.SqlId;
            model.SqlResColumn = entity.SqlResColumn;
            model.Mask = entity.Mask;
            model.Title = entity.Title;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfTextRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfTextRender = pdfRendererBase.PdfTextRenderers.Single();

            pdfTextRender.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfTextRender.TextRendererType = (byte)model.TextRendererType;
            pdfTextRender.Content = model.Content;
            pdfTextRender.SqlTemplateId = model.SqlTemplateId;
            pdfTextRender.SqlId = model.SqlId;
            pdfTextRender.SqlResColumn = model.SqlResColumn;
            pdfTextRender.Mask = model.Mask;
            pdfTextRender.Title = model.Title;

            AssignEntity(model, pdfRendererBase);
            return pdfRendererBase;
        }

        #endregion
    }
}