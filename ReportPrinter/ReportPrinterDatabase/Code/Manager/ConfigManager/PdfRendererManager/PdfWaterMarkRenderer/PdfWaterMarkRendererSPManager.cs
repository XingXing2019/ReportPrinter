using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfWaterMarkRenderer;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfWaterMarkRenderer
{
    public class PdfWaterMarkRendererSPManager : PdfRendererManagerBase<PdfWaterMarkRendererModel, Entity.PdfWaterMarkRenderer>
    {
        public override async Task Post(PdfWaterMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfWaterMarkRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.WaterMarkType,
                    model.Content,
                    (byte?)model.Location,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn,
                    model.StartPage,
                    model.EndPage,
                    model.Rotate
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf water mark renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
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
                var pdfRenderer = await Executor.ExecuteQueryOneAsync<PdfWaterMarkRendererModel>(new GetPdfWaterMarkRenderer(pdfRendererBaseId));

                if (pdfRenderer == null)
                {
                    Logger.Debug($"PDF water mark renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF water mark renderer: {pdfRendererBaseId}", procName);

                return pdfRenderer;
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
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfWaterMarkRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.WaterMarkType,
                    model.Content,
                    (byte?)model.Location,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn,
                    model.StartPage,
                    model.EndPage,
                    model.Rotate
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf water mark renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF water mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}