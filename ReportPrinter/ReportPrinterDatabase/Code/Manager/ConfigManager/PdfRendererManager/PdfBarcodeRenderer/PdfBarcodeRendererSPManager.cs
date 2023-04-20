using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer
{
    public class PdfBarcodeRendererSPManager : PdfRendererManagerBase<PdfAnnotationRendererModel>, IPdfBarcodeRendererManager
    {
        private readonly StoredProcedureExecutor _executor;

        public PdfBarcodeRendererSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = new List<StoredProcedureBase>
                {
                    new PostPdfRendererBase(
                        barcodeRenderer.PdfRendererBaseId,
                        barcodeRenderer.Id,
                        (byte)barcodeRenderer.RendererType,
                        barcodeRenderer.Margin,
                        barcodeRenderer.Padding,
                        (byte?)barcodeRenderer.HorizontalAlignment,
                        (byte?)barcodeRenderer.VerticalAlignment,
                        (byte?)barcodeRenderer.Position,
                        barcodeRenderer.Left,
                        barcodeRenderer.Right,
                        barcodeRenderer.Top,
                        barcodeRenderer.Bottom,
                        barcodeRenderer.FontSize,
                        barcodeRenderer.FontFamily,
                        (byte?)barcodeRenderer.FontStyle,
                        barcodeRenderer.Opacity,
                        (byte?)barcodeRenderer.BrushColor,
                        (byte?)barcodeRenderer.BackgroundColor,
                        barcodeRenderer.Row,
                        barcodeRenderer.Column,
                        barcodeRenderer.RowSpan,
                        barcodeRenderer.ColumnSpan
                    ),
                    new PostPdfBarcodeRenderer(
                        barcodeRenderer.PdfRendererBaseId,
                        (int?)barcodeRenderer.BarcodeFormat,
                        barcodeRenderer.ShowBarcodeText,
                        barcodeRenderer.SqlTemplateId,
                        barcodeRenderer.SqlId,
                        barcodeRenderer.SqlResColumn
                    )
                };

                var rows = await _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record pdf barcode renderer: {barcodeRenderer.PdfRendererBaseId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording PDF barcode renderer: {barcodeRenderer.PdfRendererBaseId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<PdfBarcodeRendererModel> Get(Guid pdfRendererBaseId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var pdfRendererBase = await _executor.ExecuteQueryOneAsync<PdfRendererBaseModel>(new GetPdfRendererBase(pdfRendererBaseId));
                var pdfBarcodeRenderer = await _executor.ExecuteQueryOneAsync<PdfBarcodeRendererModel>(new GetPdfBarcodeRenderer(pdfRendererBaseId));

                if (pdfRendererBase == null || pdfBarcodeRenderer == null)
                {
                    Logger.Debug($"PDF renderer: {pdfRendererBaseId} does not exist", procName);
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

        public async Task PutPdfBarcodeRenderer(PdfBarcodeRendererModel barcodeRenderer)
        {
            var procName = $"{this.GetType().Name}.{nameof(PutPdfBarcodeRenderer)}";

            try
            {
                var spList = new List<StoredProcedureBase>
                {
                    new PutPdfRendererBase(
                        barcodeRenderer.PdfRendererBaseId,
                        barcodeRenderer.Id,
                        (byte)barcodeRenderer.RendererType,
                        barcodeRenderer.Margin,
                        barcodeRenderer.Padding,
                        (byte?)barcodeRenderer.HorizontalAlignment,
                        (byte?)barcodeRenderer.VerticalAlignment,
                        (byte?)barcodeRenderer.Position,
                        barcodeRenderer.Left,
                        barcodeRenderer.Right,
                        barcodeRenderer.Top,
                        barcodeRenderer.Bottom,
                        barcodeRenderer.FontSize,
                        barcodeRenderer.FontFamily,
                        (byte?)barcodeRenderer.FontStyle,
                        barcodeRenderer.Opacity,
                        (byte?)barcodeRenderer.BrushColor,
                        (byte?)barcodeRenderer.BackgroundColor,
                        barcodeRenderer.Row,
                        barcodeRenderer.Column,
                        barcodeRenderer.RowSpan,
                        barcodeRenderer.ColumnSpan

                    ),
                    new PutPdfBarcodeRenderer(
                        barcodeRenderer.PdfRendererBaseId,
                        (int?)barcodeRenderer.BarcodeFormat,
                        barcodeRenderer.ShowBarcodeText,
                        barcodeRenderer.SqlTemplateId,
                        barcodeRenderer.SqlId,
                        barcodeRenderer.SqlResColumn
                    )
                };

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