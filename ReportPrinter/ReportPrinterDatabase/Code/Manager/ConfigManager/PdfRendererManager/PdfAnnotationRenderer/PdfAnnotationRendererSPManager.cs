using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfAnnotationRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Log;
using ZXing.Rendering;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public class PdfAnnotationRendererSPManager : PdfRendererManagerBase<PdfAnnotationRendererModel>, IPdfAnnotationRendererManager
    {
        private readonly StoredProcedureExecutor _executor;

        public PdfAnnotationRendererSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(PdfAnnotationRendererModel annotationRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(annotationRenderer);

                spList.Add(new PostPdfAnnotationRenderer(
                    annotationRenderer.PdfRendererBaseId,
                    (byte)annotationRenderer.AnnotationRendererType,
                    annotationRenderer.Title,
                    (byte?)annotationRenderer.Icon,
                    annotationRenderer.Content,
                    annotationRenderer.SqlTemplateId,
                    annotationRenderer.SqlId,
                    annotationRenderer.SqlResColumn
                ));

                var rows = await _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf annotation renderer: {annotationRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF annotation renderer: {annotationRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfAnnotationRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var pdfRendererBase = await _executor.ExecuteQueryOneAsync<PdfRendererBaseModel>(new GetPdfRendererBase(pdfRendererBaseId));
                var pdfAnnotationRenderer = await _executor.ExecuteQueryOneAsync<PdfAnnotationRendererModel>(new GetPdfAnnotationRenderer(pdfRendererBaseId));

                if (pdfRendererBase == null || pdfAnnotationRenderer == null)
                {
                    Logger.Debug($"PDF annotation renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }

                AssignRendererBaseModelProperties(pdfRendererBase, pdfAnnotationRenderer);
                Logger.Debug($"Retrieve PDF annotation renderer: {pdfRendererBaseId}", procName);

                return pdfAnnotationRenderer;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF annotation renderer: {pdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PutPdfAnnotationRenderer(PdfAnnotationRendererModel annotationRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutPdfBarcodeRenderer)}";

            try
            {
                var spList = CreatePutStoreProcedures(annotationRenderer);
                spList.Add(new PutPdfAnnotationRenderer(
                    annotationRenderer.PdfRendererBaseId,
                    (byte)annotationRenderer.AnnotationRendererType,
                    annotationRenderer.Title,
                    (byte?)annotationRenderer.Icon,
                    annotationRenderer.Content,
                    annotationRenderer.SqlTemplateId,
                    annotationRenderer.SqlId,
                    annotationRenderer.SqlResColumn
                ));

                var rows = _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf annotation renderer: {annotationRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF annotation renderer: {annotationRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}