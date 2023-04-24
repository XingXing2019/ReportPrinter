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
                var pdfRendererBase = CreateEntity(model);

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

                var entity = await context.PdfRendererBases
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfWaterMarkRenderers)
                    .Include(x => x.PdfWaterMarkRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlTemplateConfig)
                    .Include(x => x.PdfWaterMarkRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlConfig)
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
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfWaterMarkRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF water mark renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
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

        protected override PdfWaterMarkRendererModel CreateDataModel(PdfRendererBase entity)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfWaterMarkRenderers.Single();

            model.WaterMarkType = (WaterMarkRendererType)renderer.WaterMarkType;
            model.Content = renderer.Content;
            model.Location = (Location?)renderer.Location;
            
            model.SqlTemplateConfigSqlConfigId = renderer.SqlTemplateConfigSqlConfigId;
            model.SqlTemplateId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlTemplateConfig.Id : null;
            model.SqlId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlConfig.Id : null;
            model.SqlResColumn = entity.SqlResColumnConfigs.SingleOrDefault()?.Name;

            model.StartPage = renderer.StartPage;
            model.EndPage = renderer.EndPage;
            model.Rotate = renderer.Rotate;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfWaterMarkRendererModel model)
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

            pdfRendererBase.PdfWaterMarkRenderers.Add(new Entity.PdfWaterMarkRenderer
            {
                PdfRendererBaseId = model.PdfRendererBaseId,
                WaterMarkType = (byte)model.WaterMarkType,
                Content = model.Content,
                Location = (byte?)model.Location,
                SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId,
                StartPage = model.StartPage,
                EndPage = model.EndPage,
                Rotate = model.Rotate
            });

            return pdfRendererBase;
        }

        protected override void UpdateEntity(PdfRendererBase pdfRendererBase, PdfWaterMarkRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);

            var renderer = pdfRendererBase.PdfWaterMarkRenderers.Single();
            renderer.WaterMarkType = (byte)model.WaterMarkType;
            renderer.Content = model.Content;
            renderer.Location = (byte?)model.Location;
            renderer.SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId;
            renderer.StartPage = model.StartPage;
            renderer.EndPage = model.EndPage;
            renderer.Rotate = model.Rotate;

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