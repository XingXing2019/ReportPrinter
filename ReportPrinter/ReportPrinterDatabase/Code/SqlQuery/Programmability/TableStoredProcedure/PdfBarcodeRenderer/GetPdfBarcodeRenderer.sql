IF OBJECT_ID('[dbo].[GetPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[GetPdfBarcodeRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT
		[PBR_PdfBarcodeRendererId] AS PdfBarcodeRendererId,
		[PBR_PdfRendererBaseId] AS PdfRendererBaseId,
		[PBR_BarcodeFormat] AS BarcodeFormat,
		[PBR_ShowBarcodeText] AS ShowBarcodeText,
		[PBR_SqlTemplateId] AS SqlTemplateId,
		[PBR_SqlId] AS SqlId,
		[PBR_SqlResColumn] AS SqlResColumn
	FROM
		[PdfBarcodeRenderer]
	WHERE
		[PBR_PdfRendererBaseId] = @pdfRendererBaseId		
END