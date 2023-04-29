using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Context;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTableRenderer
{
    public class PdfTableRendererEFCoreManager : PdfRendererManagerBase<PdfTableRendererModel>
    {
        public override async Task Post(PdfTableRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                await using var context = new ReportPrinterContext();
                var pdfRendererBase = CreateEntity(model);

                context.PdfRendererBases.Add(pdfRendererBase);
                var rows = await context.SaveChangesAsync();
                Logger.Debug($"Record pdf table renderer: {pdfRendererBase.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF table renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfTableRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var entity = await GetPdfTableRenderer(pdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF table renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                var dict = new Dictionary<Guid, PdfRendererBase>
                {
                    {pdfRendererBaseId, entity}
                };

                var point = entity;
                while (point.PdfTableRenderers.Single().SubPdfTableRendererId.HasValue)
                {
                    point = await GetPdfTableRenderer(point.PdfTableRenderers.Single().SubPdfTableRendererId.Value);
                    dict[point.PdfRendererBaseId] = point;
                }
                
                Logger.Debug($"Retrieve PDF table renderer: {pdfRendererBaseId}", procName);
                return CreateDataModel(entity, dict);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF table renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfTableRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                await using var context = new ReportPrinterContext();

                var entity = await context.PdfRendererBases
                    .Include(x => x.SqlResColumnConfigs)
                    .Include(x => x.PdfTableRenderers)
                    .FirstOrDefaultAsync(x => x.PdfRendererBaseId == model.PdfRendererBaseId);

                if (entity == null)
                {
                    Logger.Debug($"PDF table renderer: {model.PdfRendererBaseId} does not exist", procName);
                }
                else
                {
                    UpdateEntity(entity, model);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update pdf table renderer: {entity.PdfRendererBaseId}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF table renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        private async Task<PdfRendererBase> GetPdfTableRenderer(Guid pdfRendererBaseId)
        {
            await using var context = new ReportPrinterContext();

            var entity = await context.PdfRendererBases
                .Include(x => x.SqlResColumnConfigs)
                .Include(x => x.PdfTableRenderers)
                .Include(x => x.PdfTableRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlTemplateConfig)
                .Include(x => x.PdfTableRenderers).ThenInclude(x => x.SqlTemplateConfigSqlConfig).ThenInclude(x => x.SqlConfig)
                .FirstOrDefaultAsync(x => x.PdfRendererBaseId == pdfRendererBaseId);

            return entity;
        }

        protected PdfTableRendererModel CreateDataModel(PdfRendererBase entity, Dictionary<Guid, PdfRendererBase> dict)
        {
            var model = CreateRendererBaseDataModel(entity);
            var renderer = entity.PdfTableRenderers.Single();
            
            model.BoardThickness = renderer.BoardThickness;
            model.LineSpace = renderer.LineSpace;
            model.TitleHorizontalAlignment = renderer.TitleHorizontalAlignment.HasValue ? (HorizontalAlignment?)renderer.TitleHorizontalAlignment : null;
            model.HideTitle = renderer.HideTitle;
            model.Space = renderer.Space;
            model.TitleColor = renderer.TitleColor.HasValue ? (XKnownColor?)renderer.TitleColor : null;
            model.TitleColorOpacity = renderer.TitleColorOpacity;
            model.SqlTemplateConfigSqlConfigId = renderer.SqlTemplateConfigSqlConfigId;
            model.SqlTemplateId = renderer.SqlTemplateConfigSqlConfig.SqlTemplateConfig.Id;
            model.SqlId = renderer.SqlTemplateConfigSqlConfig.SqlConfig.Id;
            model.SqlResColumns = entity.SqlResColumnConfigs.Select(x => new SqlResColumnModel
            {
                PdfRendererBaseId = x.PdfRendererBaseId,
                Id = x.Id,
                Title = x.Title,
                WidthRatio = x.WidthRatio,
                Position = x.Position.HasValue ? (Position?)x.Position : null,
                Left = x.Left,
                Right = x.Right
            }).ToList();
            model.SqlVariable = renderer.SqlVariable;
            model.SubPdfTableRendererId = renderer.SubPdfTableRendererId;
            model.SubPdfTableRenderer = CreateSubPdfTableRender(entity, dict);

            return model;
        }
        
        private PdfTableRendererModel CreateSubPdfTableRender(PdfRendererBase entity, Dictionary<Guid, PdfRendererBase> dict)
        {
            var renderer = entity.PdfTableRenderers.Single();
            if (!renderer.SubPdfTableRendererId.HasValue)
                return null;

            return CreateDataModel(dict[renderer.SubPdfTableRendererId.Value], dict);
        }

        protected override PdfRendererBase CreateEntity(PdfTableRendererModel model)
        {
            var pdfRendererBase = new PdfRendererBase();
            AssignRendererBaseProperties(model, pdfRendererBase);

            foreach (var sqlResColumn in model.SqlResColumns)
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Id = sqlResColumn.Id,
                    Title = sqlResColumn.Title,
                    WidthRatio = sqlResColumn.WidthRatio,
                    Position = sqlResColumn.Position.HasValue ? (byte?)sqlResColumn.Position : null,
                    Left = sqlResColumn.Left,
                    Right = sqlResColumn.Right,
                });
            }

            pdfRendererBase.PdfTableRenderers.Add(new Entity.PdfTableRenderer
            {
                PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                BoardThickness = model.BoardThickness,
                LineSpace = model.LineSpace,
                TitleHorizontalAlignment = model.TitleHorizontalAlignment.HasValue ? (byte?)model.TitleHorizontalAlignment : null,
                HideTitle = model.HideTitle,
                Space = model.Space,
                TitleColor = model.TitleColor.HasValue ? (byte?)model.TitleColor : null,
                TitleColorOpacity = model.TitleColorOpacity,
                SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId,
                SqlVariable = model.SqlVariable,
                SubPdfTableRendererId = model.SubPdfTableRendererId
            });

            return pdfRendererBase;
        }

        protected override void UpdateEntity(PdfRendererBase pdfRendererBase, PdfTableRendererModel model)
        {
            AssignRendererBaseProperties(model, pdfRendererBase);

            var renderer = pdfRendererBase.PdfTableRenderers.Single();

            renderer.BoardThickness = model.BoardThickness;
            renderer.LineSpace = model.LineSpace;
            renderer.TitleHorizontalAlignment = model.TitleHorizontalAlignment.HasValue ? (byte?)model.TitleHorizontalAlignment : null;
            renderer.HideTitle = model.HideTitle;
            renderer.Space = model.Space;
            renderer.TitleColor = model.TitleColor.HasValue ? (byte?)model.TitleColor : null;
            renderer.TitleColorOpacity = model.TitleColorOpacity;
            renderer.SqlTemplateConfigSqlConfigId = model.SqlTemplateConfigSqlConfigId;
            renderer.SqlVariable = model.SqlVariable;
            renderer.SubPdfTableRendererId = model.SubPdfTableRendererId;

            pdfRendererBase.SqlResColumnConfigs.Clear();

            foreach (var sqlResColumn in model.SqlResColumns)
            {
                pdfRendererBase.SqlResColumnConfigs.Add(new SqlResColumnConfig
                {
                    PdfRendererBaseId = pdfRendererBase.PdfRendererBaseId,
                    Id = sqlResColumn.Id,
                    Title = sqlResColumn.Title,
                    WidthRatio = sqlResColumn.WidthRatio,
                    Position = sqlResColumn.Position.HasValue ? (byte?)sqlResColumn.Position : null,
                    Left = sqlResColumn.Left,
                    Right = sqlResColumn.Right,
                });
            }
        }

        #endregion
    }
}