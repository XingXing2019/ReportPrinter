using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer
{
    public class PdfBarcodeRendererEFCoreManager : PdfRendererManagerBase<PdfBarcodeRendererModel, Entity.PdfBarcodeRenderer>
    {
        public override async Task Post(PdfBarcodeRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var pdfRendererBase = new PdfRendererBase();
                pdfRendererBase.PdfBarcodeRenderers.Add(new Entity.PdfBarcodeRenderer());
                pdfRendererBase = CreateEntity(model, pdfRendererBase);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf barcode renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF barcode renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfBarcodeRendererModel> Get(Guid pdfRendererBaseId)
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

        public override async Task Put(PdfBarcodeRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var entity = await context.PdfRendererBases
                    .Include(x => x.PdfBarcodeRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF barcode renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    entity = CreateEntity(model, entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf barcode renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF barcode renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        protected override PdfBarcodeRendererModel CreateDataModel(Entity.PdfBarcodeRenderer entity)
        {
            var model = CreateRendererBaseDataModel(entity.PdfRendererBase);
            
            model.ShowBarcodeText = entity.ShowBarcodeText;
            model.SqlTemplateId = entity.SqlTemplateId;
            model.SqlId = entity.SqlId;
            model.SqlResColumn = entity.SqlResColumn;
            
            if (entity.BarcodeFormat.HasValue)
                model.BarcodeFormat = (BarcodeFormat)entity.BarcodeFormat.Value;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfBarcodeRendererModel model, PdfRendererBase pdfRendererBase)
        {
            var pdfBarcodeRenderer = pdfRendererBase.PdfBarcodeRenderers.Single();

            pdfBarcodeRenderer.PdfRendererBaseId = model.PdfRendererBaseId;
            pdfBarcodeRenderer.BarcodeFormat = model.BarcodeFormat.HasValue ? (int?)model.BarcodeFormat.Value : null;
            pdfBarcodeRenderer.ShowBarcodeText = model.ShowBarcodeText;
            pdfBarcodeRenderer.SqlTemplateId = model.SqlTemplateId;
            pdfBarcodeRenderer.SqlId = model.SqlId;
            pdfBarcodeRenderer.SqlResColumn = model.SqlResColumn;

            AssignRendererBaseProperties(model, pdfRendererBase);
            return pdfRendererBase;
        }

        #endregion
    }
}