using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer
{
    public class PdfBarcodeRendererSPManager : PdfRendererManagerBase<PdfBarcodeRendererModel>
    {
        private readonly StoredProcedureExecutor _executor;

        public PdfBarcodeRendererSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public override async Task Post(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(barcodeRenderer);

                spList.Add(new PostPdfBarcodeRenderer(
                    barcodeRenderer.PdfRendererBaseId,
                    (int?)barcodeRenderer.BarcodeFormat,
                    barcodeRenderer.ShowBarcodeText,
                    barcodeRenderer.SqlTemplateId,
                    barcodeRenderer.SqlId,
                    barcodeRenderer.SqlResColumn
                ));

                var rows = await _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf barcode renderer: {barcodeRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task<PdfBarcodeRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var pdfRendererBase = await _executor.ExecuteQueryOneAsync<PdfRendererBaseModel>(new GetPdfRendererBase(pdfRendererBaseId));
                var pdfBarcodeRenderer = await _executor.ExecuteQueryOneAsync<PdfBarcodeRendererModel>(new GetPdfBarcodeRenderer(pdfRendererBaseId));

                if (pdfRendererBase == null || pdfBarcodeRenderer == null)
                {
                    Logger.Debug($"PDF barcode renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                AssignRendererBaseModelProperties(pdfRendererBase, pdfBarcodeRenderer);
                Logger.Debug($"Retrieve PDF barcode renderer: {pdfRendererBaseId}", procName);

                return pdfBarcodeRenderer;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF barcode renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public override async Task Put(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Put)}";

            try
            {
                var spList = CreatePutStoreProcedures(barcodeRenderer);
                spList.Add(new PutPdfBarcodeRenderer(
                    barcodeRenderer.PdfRendererBaseId,
                    (int?)barcodeRenderer.BarcodeFormat,
                    barcodeRenderer.ShowBarcodeText,
                    barcodeRenderer.SqlTemplateId,
                    barcodeRenderer.SqlId,
                    barcodeRenderer.SqlResColumn
                ));

                var rows = _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf barcode renderer: {barcodeRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}