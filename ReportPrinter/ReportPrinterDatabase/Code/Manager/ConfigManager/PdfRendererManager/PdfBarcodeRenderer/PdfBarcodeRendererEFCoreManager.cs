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
                var pdfRendererBase = CreateEntity(model);

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

                var entity = await context.PdfRendererBases
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfBarcodeRenderers)
                    .Include(x => x.PdfBarcodeRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlTemplateConfig)
                    .Include(x => x.PdfBarcodeRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlConfig)
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
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfBarcodeRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF barcode renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
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

        protected override PdfBarcodeRendererModel CreateDataModel(PdfRendererBase entity)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfBarcodeRenderers.Single();

            model.BarcodeFormat = renderer.BarcodeFormat.HasValue ? (BarcodeFormat?)renderer.BarcodeFormat.Value : null;
            model.ShowBarcodeText = renderer.ShowBarcodeText;
            model.SqlTemplateConfigSqlConfigId = renderer.SqlTemplateConfigSqlConfigId;
            model.SqlTemplateId = renderer.SqlTemplateConfigSqlConfig.SqlTemplateConfig.Id;
            model.SqlId = renderer.SqlTemplateConfigSqlConfig.SqlConfig.Id;
            model.SqlResColumn = entity.SqlResColumnConfigs.SingleOrDefault()?.Name;
            
            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfBarcodeRendererModel model)
        {
            var pdfRendererBase = new PdfRendererBase();
            AssignRendererBaseProperties(model, pdfRendererBase);

            if (!string.IsNullOrEmpty(model.SqlResColumn))
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Name = model.SqlResColumn
                });
            }

            pdfRendererBase.PdfBarcodeRenderers.Add(new Entity.PdfBarcodeRenderer
            {
                PdfRendererBaseId = model.PdfRendererBaseId,
                BarcodeFormat = model.BarcodeFormat.HasValue ? (int?)model.BarcodeFormat.Value : null,
                ShowBarcodeText = model.ShowBarcodeText,
                SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId
            });

            return pdfRendererBase;
        }

        protected override void UpdateEntity(PdfRendererBase pdfRendererBase, PdfBarcodeRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);

            var renderer = pdfRendererBase.PdfBarcodeRenderers.Single();
            renderer.BarcodeFormat = model.BarcodeFormat.HasValue ? (int?)model.BarcodeFormat.Value : null;
            renderer.ShowBarcodeText = model.ShowBarcodeText;
            renderer.SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId;

            pdfRendererBase.SqlResColumnConfigs.Clear();

            if (!string.IsNullOrEmpty(model.SqlResColumn))
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Name = model.SqlResColumn
                });
            }
        }

        #endregion
    }
}