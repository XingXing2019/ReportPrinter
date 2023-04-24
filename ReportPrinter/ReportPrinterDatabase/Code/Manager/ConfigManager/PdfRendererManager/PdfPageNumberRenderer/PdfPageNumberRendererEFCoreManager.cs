using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfPageNumberRenderer
{
    public class PdfPageNumberRendererEFCoreManager : PdfRendererManagerBase<PdfPageNumberRendererModel, Entity.PdfPageNumberRenderer>
    {
        public override async Task Post(PdfPageNumberRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var pdfRendererBase = CreateEntity(model);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf page number renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF page number renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfPageNumberRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfPageNumberRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF page number renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF page number renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF page number renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfPageNumberRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfPageNumberRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF page number renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf page number renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF page number renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfPageNumberRendererModel CreateDataModel(PdfRendererBase entity)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfPageNumberRenderers.Single();

            model.StartPage = renderer.StartPage;
            model.EndPage = renderer.EndPage;
            model.PageNumberLocation = (Location?)renderer.PageNumberLocation;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfPageNumberRendererModel model)
        {
            var pdfRendererBase = new PdfRendererBase();
            AssignRendererBaseProperties(model, pdfRendererBase);

            pdfRendererBase.PdfPageNumberRenderers.Add(new Entity.PdfPageNumberRenderer
            {
                PdfRendererBaseId = model.PdfRendererBaseId,
                StartPage = model.StartPage,
                EndPage = model.EndPage,
                PageNumberLocation = (byte?)model.PageNumberLocation,
            });

            return pdfRendererBase;
        }

        protected override void UpdateEntity(PdfRendererBase pdfRendererBase, PdfPageNumberRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);
            var renderer = pdfRendererBase.PdfPageNumberRenderers.Single();

            renderer.StartPage = model.StartPage;
            renderer.EndPage = model.EndPage;
            renderer.PageNumberLocation = (byte?)model.PageNumberLocation;
        }

        #endregion
    }
}