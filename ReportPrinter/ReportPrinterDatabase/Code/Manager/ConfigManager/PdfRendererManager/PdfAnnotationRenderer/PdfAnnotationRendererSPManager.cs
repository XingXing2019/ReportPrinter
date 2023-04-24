using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfAnnotationRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer
{
    public class PdfAnnotationRendererSPManager : PdfRendererManagerBase<PdfAnnotationRendererModel, Entity.PdfAnnotationRenderer>
    {
        public override async Task Post(PdfAnnotationRendererModel model)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = CreatePostStoreProcedures(model);

                spList.Add(new PostPdfAnnotationRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.AnnotationRendererType,
                    model.Title,
                    (byte?)model.Icon,
                    model.Content,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf annotation renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
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
                var pdfRenderer = await Executor.ExecuteQueryOneAsync<PdfAnnotationRendererModel>(new GetPdfAnnotationRenderer(pdfRendererBaseId));

                if (pdfRenderer == null)
                {
                    Logger.Debug($"PDF annotation renderer: {pdfRendererBaseId} does not exist", procName);
                    return null;
                }
                
                Logger.Debug($"Retrieve PDF annotation renderer: {pdfRendererBaseId}", procName);

                return pdfRenderer;
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
                var spList = CreatePutStoreProcedures(model);
                spList.Add(new PutPdfAnnotationRenderer(
                    model.PdfRendererBaseId,
                    (byte)model.AnnotationRendererType,
                    model.Title,
                    (byte?)model.Icon,
                    model.Content,
                    model.SqlTemplateConfigSqlConfigId,
                    model.SqlResColumn
                ));

                var rows = await Executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Update pdf annotation renderer: {model.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating PDF annotation renderer: {model.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}