using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer
{
    public class PdfReprintMarkRendererEFCoreManager : PdfRendererManagerBase<PdfReprintMarkRendererModel, Entity.PdfReprintMarkRenderer>
    {
        public override async Task Post(PdfReprintMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfReprintMarkRenderers.Add(new Entity.PdfReprintMarkRenderer());

                pdfRendererBase = CreateEntity(model, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf reprint mark renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF reprint mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfReprintMarkRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfReprintMarkRenderers
                    .Include(x => x.PdfRendererBase)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF reprint mark renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF reprint mark renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF reprint mark renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfReprintMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfReprintMarkRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF reprint mark renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(model, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf reprint mark renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF reprint mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfReprintMarkRendererModel CreateDataModel(Entity.PdfReprintMarkRenderer entity)
        {
            var model = CreateRendererBaseDataModel(entity.PdfRendererBase);

            model.Text = entity.Text;
            model.BoardThickness = entity.BoardThickness;
            model.Location = (Location?)entity.Location;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfReprintMarkRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfReprintMarkRender = pdfRendererBase.PdfReprintMarkRenderers.Single();

            pdfReprintMarkRender.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfReprintMarkRender.Text = model.Text;
            pdfReprintMarkRender.BoardThickness = model.BoardThickness;
            pdfReprintMarkRender.Location = (byte?)model.Location;

            AssignRendererBaseProperties(model, pdfRendererBase);
            return pdfRendererBase;
        }

        #endregion
    }
}