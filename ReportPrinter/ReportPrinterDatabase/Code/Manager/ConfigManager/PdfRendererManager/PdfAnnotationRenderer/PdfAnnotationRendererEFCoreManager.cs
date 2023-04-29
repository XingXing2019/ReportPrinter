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
    public class PdfAnnotationRendererEFCoreManager : PdfRendererManagerBase<PdfAnnotationRendererModel>
    {
        public override async Task Post(PdfAnnotationRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var pdfRendererBase = CreateEntity(model);

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

                var entity = await context.PdfRendererBases
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfAnnotationRenderers)
                    .Include(x => x.PdfAnnotationRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlTemplateConfig)
                    .Include(x => x.PdfAnnotationRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlConfig)
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
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfAnnotationRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF annotation renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
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

        protected override PdfAnnotationRendererModel CreateDataModel(PdfRendererBase entity)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfAnnotationRenderers.Single();

            model.AnnotationRendererType = (AnnotationRendererType)renderer.AnnotationRendererType;
            model.Title = renderer.Title;
            model.Icon = renderer.Icon.HasValue ? (PdfTextAnnotationIcon?)renderer.Icon.Value : null;
            model.Content = renderer.Content;
            model.SqlTemplateConfigSqlConfigId = renderer.SqlTemplateConfigSqlConfigId;
            model.SqlTemplateId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlTemplateConfig.Id : null;
            model.SqlId = renderer.SqlTemplateConfigSqlConfigId.HasValue ? renderer.SqlTemplateConfigSqlConfig.SqlConfig.Id : null;
            model.SqlResColumn = entity.SqlResColumnConfigs.SingleOrDefault()?.Id;

            return model;
        }

        protected override PdfRendererBase CreateEntity(PdfAnnotationRendererModel model)
        {
            var pdfRendererBase = new PdfRendererBase();
            AssignRendererBaseProperties(model, pdfRendererBase);

            if (!string.IsNullOrEmpty(model.SqlResColumn))
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Id = model.SqlResColumn
                });
            }

            pdfRendererBase.PdfAnnotationRenderers.Add(new Entity.PdfAnnotationRenderer
            {
                PdfRendererBaseId = model.PdfRendererBaseId,
                AnnotationRendererType = (byte)model.AnnotationRendererType,
                Title = model.Title,
                Icon = model.Icon.HasValue ? (byte?)model.Icon : null,
                Content = model.Content,
                SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId
            });
            
            return pdfRendererBase;
        }

        protected override void UpdateEntity(PdfRendererBase pdfRendererBase, PdfAnnotationRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);

            var renderer = pdfRendererBase.PdfAnnotationRenderers.Single();
            renderer.AnnotationRendererType = (byte)model.AnnotationRendererType;
            renderer.Title = model.Title;
            renderer.Icon = model.Icon.HasValue ? (byte?)model.Icon : null;
            renderer.Content = model.Content;
            renderer.SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId;
            
            pdfRendererBase.SqlResColumnConfigs.Clear();

            if (!string.IsNullOrEmpty(model.SqlResColumn))
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Id = model.SqlResColumn
                });
            }
        }

        #endregion
    }
}