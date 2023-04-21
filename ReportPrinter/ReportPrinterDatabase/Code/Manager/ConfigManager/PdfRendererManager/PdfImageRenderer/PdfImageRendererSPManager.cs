using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfImageRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer
{
    public class PdfImageRendererSPManager : PdfRendererManagerBase<PdfImageRendererModel>
    {
        public override async Task Post(PdfImageRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfImageRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.SourceType,
                    model.ImageSource
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf image renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF image renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfImageRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var pdfRendererBase = await Executor.ExecuteQueryOneAsync<PdfRendererBaseModel>(new GetPdfRendererBase(pdfRendererBaseId));
                var pdfBarcodeRenderer = await Executor.ExecuteQueryOneAsync<PdfImageRendererModel>(new GetPdfImageRenderer(pdfRendererBaseId));

                if (pdfRendererBase == null || pdfBarcodeRenderer == null)
                {
                    Logger.Debug($"PDF image renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                AssignRendererBaseModelProperties(pdfRendererBase, pdfBarcodeRenderer);
                Logger.Debug($"Retrieve PDF image renderer: {pdfRendererBaseId}", procName);

                return pdfBarcodeRenderer;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF image renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfImageRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfImageRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.SourceType,
                    model.ImageSource
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf image renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF image renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}