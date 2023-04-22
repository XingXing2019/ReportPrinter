using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterDatabase.Code.StoredProcedures.PdfReprintMarkRenderer;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer
{
    public class PdfReprintMarkRendererSPManager : PdfRendererManagerBase<PdfReprintMarkRendererModel, Entity.PdfReprintMarkRenderer>
    {
        public override async Task Post(PdfReprintMarkRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfReprintMarkRenderer(
                    model.PdfRendererBaseId,
                    model.Text,
                    model.BoardThickness,
                    (byte?)model.Location
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf reprint mark renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
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
                var pdfRenderer = await Executor.ExecuteQueryOneAsync<PdfReprintMarkRendererModel>(new GetPdfReprintMarkRenderer(pdfRendererBaseId));

                if (pdfRenderer == null)
                {
                    Logger.Debug($"PDF reprint mark renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }
                
                Logger.Debug($"Retrieve PDF reprint mark renderer: {pdfRendererBaseId}", procName);

                return pdfRenderer;
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
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfReprintMarkRenderer(
                    model.PdfRendererBaseId,
                    model.Text,
                    model.BoardThickness,
                    (byte?)model.Location
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf reprint mark renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF reprint mark renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}