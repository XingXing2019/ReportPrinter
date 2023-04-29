using System;
using System.Linq;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfTableRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.SqlResColumnConfig;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTableRenderer
{
    public class PdfTableRendererSPManager : PdfRendererManagerBase<PdfTableRendererModel>
    {
        public override async Task Post(PdfTableRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfTableRenderer(
                    model.PdfRendererBaseId,
                    model.BoardThickness,
                    model.LineSpace,
                    (byte?)model.TitleHorizontalAlignment,
                    model.HideTitle,
                    model.Space,
                    (byte?)model.TitleColor,
                    model.TitleColorOpacity,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlVariable,
                    model.SubPdfTableRendererId
                ));

                foreach (var sqlResColumn in model.SqlResColumns)
                {
                    spList.Add(new PostSqlResColumnConfig(
                        model.PdfRendererBaseId,
                        sqlResColumn.Id,
                        sqlResColumn.Title,
                        sqlResColumn.WidthRatio,
                        (byte?)sqlResColumn.Position,
                        sqlResColumn.Left,
                        sqlResColumn.Right
                    ));
                }

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf table renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
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
                var pdfRenderers = await Executor.ExecuteQueryBatchAsync<PdfTableRendererModel>(new GetPdfTableRenderer(pdfRendererBaseId));
                var sqlResColumns = await Executor.ExecuteQueryBatchAsync<SqlResColumnModel>(new GetSqlResColumnConfig(pdfRendererBaseId));

                if (pdfRenderers.Count == 0)
                {
                    Logger.Debug($"PDF table renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }
                
                Logger.Debug($"Retrieve PDF table renderer: {pdfRendererBaseId}", procName);

                var pdfRenderer = pdfRenderers.Single(x => x.PdfRendererBaseId == pdfRendererBaseId);
                var point = pdfRenderer;

                while (point != null)
                {
                    var subPdfRenderer = pdfRenderers.SingleOrDefault(x => x.PdfRendererBaseId == point.SubPdfTableRendererId);
                    point.SubPdfTableRenderer = subPdfRenderer;
                    point.SqlResColumns = sqlResColumns.Where(x => x.PdfRendererBaseId == point.PdfRendererBaseId).ToList();
                    point = subPdfRenderer;
                }

                return pdfRenderer;
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
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfTableRenderer(
                    model.PdfRendererBaseId,
                    model.BoardThickness,
                    model.LineSpace,
                    (byte?)model.TitleHorizontalAlignment,
                    model.HideTitle,
                    model.Space,
                    (byte?)model.TitleColor,
                    model.TitleColorOpacity,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlVariable,
                    model.SubPdfTableRendererId
                ));

                spList.Add(new DeleteSqlResColumnConfig(model.PdfRendererBaseId));

                foreach (var sqlResColumn in model.SqlResColumns)
                {
                    spList.Add(new PostSqlResColumnConfig(
                        model.PdfRendererBaseId,
                        sqlResColumn.Id,
                        sqlResColumn.Title,
                        sqlResColumn.WidthRatio,
                        (byte?)sqlResColumn.Position,
                        sqlResColumn.Left,
                        sqlResColumn.Right
                    ));
                }

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf table renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF table renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}