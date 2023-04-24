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
                var pdfRendererBase = CreateEntity(model);

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

                var entity = await context.PdfRendererBases
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfTextRenderers)
                    .Include(x => x.PdfTextRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlTemplateConfig)
                    .Include(x => x.PdfTextRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlConfig)
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
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfTextRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF text renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
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

        protected PdfTextRendererModel CreateDataModel(PdfRendererBase entity)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfTextRenderers.Single();

            model.TextRendererType = (TextRendererType)renderer.TextRendererType;
            model.Content = renderer.Content;
            
            model.SqlTemplateConfigSqlConfigId = renderer.SqlTemplateConfigSqlConfigId;
            model.SqlTemplateId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlTemplateConfig.Id : null;
            model.SqlId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlConfig.Id : null;
            model.SqlResColumn = entity.SqlResColumnConfigs.SingleOrDefault()?.Name;

            model.Mask = renderer.Mask;
            model.Title = renderer.Title;

            return model;
        }

        protected PdfRendererBase CreateEntity(PdfTextRendererModel model)
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

            pdfRendererBase.PdfTextRenderers.Add(new Entity.PdfTextRenderer
            {
                PdfRendererBaseId = model.PdfRendererBaseId,
                TextRendererType = (byte)model.TextRendererType,
                Content = model.Content,
                SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId,
                Mask = model.Mask,
                Title = model.Title,
            });

            return pdfRendererBase;
        }

        protected void UpdateEntity(PdfRendererBase pdfRendererBase, PdfTextRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);

            var renderer = pdfRendererBase.PdfTextRenderers.Single();
            renderer.TextRendererType = (byte)model.TextRendererType;
            renderer.Content = model.Content;
            renderer.SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId;
            renderer.Mask = model.Mask;
            renderer.Title = model.Title;

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