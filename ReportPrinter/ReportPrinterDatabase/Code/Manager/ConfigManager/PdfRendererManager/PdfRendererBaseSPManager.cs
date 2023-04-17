using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager
{
    public class PdfRendererBaseSPManager : IPdfRendererBaseManager
    {
        private readonly StoredProcedureExecutor _executor;

        public PdfRendererBaseSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }


        public async Task Post(PdfRendererBaseModel pdfRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var pdfRendererBaseId = pdfRenderer.PdfRendererBaseId;
                var id = pdfRenderer.Id;
                var rendererType = (byte)pdfRenderer.RendererType;
                var row = pdfRenderer.Row;
                var column = pdfRenderer.Column;

                var sp = new PostPdfRendererBase(pdfRendererBaseId, id, rendererType, row, column);
                var rows = await _executor.ExecuteNonQueryAsync(sp);
                Logger.Debug($"Record PDF renderer: {pdfRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF renderer: {pdfRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfRendererBaseModel> Get(Guid pdfRendererId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var sp = new GetPdfRendererBase(pdfRendererId);
                var entity = await _executor.ExecuteQueryOneAsync<PdfRendererBaseModel>(sp);

                if (entity == null)
                {
                    Logger.Debug($"PDF renderer: {pdfRendererId} does not exist", procName);
                    return null;
                }

                Logger.Debug($"Retrieve PDF renderer: {pdfRendererId}", procName);
                return entity;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving PDF renderer: {pdfRendererId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IList<PdfRendererBaseModel>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            try
            {
                var sp = new GetAllPdfRendererBase();
                var entities = await _executor.ExecuteQueryBatchAsync<PdfRendererBaseModel>(sp);

                Logger.Debug($"Retrieve all PDF renderers", procName);
                return entities;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(Guid pdfRendererId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var sp = new DeletePdfRendererBaseById(pdfRendererId);
                var rows = await _executor.ExecuteNonQueryAsync(sp);
                Logger.Debug($"Delete PDF renderer: {pdfRendererId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderer: {pdfRendererId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(List<Guid> pdfRendererIds)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                if (pdfRendererIds == null || pdfRendererIds.Count == 0)
                {
                    Logger.Debug($"PDF renderers does not exist", procName);
                }
                else
                {
                    var sp = new DeletePdfRendererBaseByIds(string.Join(',', pdfRendererIds));
                    var rows = await _executor.ExecuteNonQueryAsync(sp);
                    Logger.Debug($"Delete PDF renderers, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                var sp = new DeleteAllPdfRendererBase();
                var rows = await _executor.ExecuteNonQueryAsync(sp);
                Logger.Debug($"Delete PDF renderers, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting PDF renderers. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<PdfRendererBaseModel>> GetAllByRendererType(PdfRendererType rendererType)
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAllByRendererType)}";

            try
            {
                var sp = new GetAllByRendererType((byte)rendererType);
                var entities = await _executor.ExecuteQueryBatchAsync<PdfRendererBaseModel>(sp);
                Logger.Debug($"Retrieve all PDF renderers by with type: {rendererType}", procName);
                return entities;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderer with type: {rendererType}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<List<PdfRendererBaseModel>> GetAlByRendererIdPrefix(string rendererIdPrefix)
        {
            var procName = $"{this.GetType()}.{nameof(GetAlByRendererIdPrefix)}";

            try
            {
                var sp = new GetAllByRendererIdPrefix(rendererIdPrefix);
                var entities = await _executor.ExecuteQueryBatchAsync<PdfRendererBaseModel>(sp);

                Logger.Debug($"Retrieve all PDF renderers by renderer id prefix: {rendererIdPrefix}", procName);
                return entities;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all PDF renderers by renderer id prefix: {rendererIdPrefix}. Ex: {ex.Message}", procName);
                throw;
            }
        }
    }
}