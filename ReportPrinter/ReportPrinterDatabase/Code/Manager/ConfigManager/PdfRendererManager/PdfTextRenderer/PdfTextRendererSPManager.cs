using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfTextRenderer;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTextRenderer
{
    public class PdfTextRendererSPManager : PdfRendererManagerBase<PdfTextRendererModel>
    {
        public override async Task Post(PdfTextRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfTextRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.TextRendererType,
                    model.Content,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn,
                    model.Mask,
                    model.Title
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf text renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
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
                var pdfRenderer = await Executor.ExecuteQueryOneAsync<PdfTextRendererModel>(new GetPdfTextRenderer(pdfRendererBaseId));

                if (pdfRenderer == null)
                {
                    Logger.Debug($"PDF text renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF text renderer: {pdfRendererBaseId}", procName);

                return pdfRenderer;
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
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfTextRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.TextRendererType,
                    model.Content,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn,
                    model.Mask,
                    model.Title
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf text renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF text renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}