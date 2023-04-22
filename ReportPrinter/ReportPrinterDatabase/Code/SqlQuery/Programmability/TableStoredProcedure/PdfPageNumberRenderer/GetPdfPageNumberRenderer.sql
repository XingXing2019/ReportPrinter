IF OBJECT_ID('[dbo].[GetPdfPageNumberRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfPageNumberRenderer]
END
GO

CREATE PROCEDURE [dbo].[GetPdfPageNumberRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT
		[PPNR_PdfPageNumberRendererId] AS PdfPageNumberRendererId,
		[PPNR_PdfRendererBaseId] AS PdfRendererBaseId,
		[PPNR_StartPage] AS StartPage,
		[PPNR_EndPage] AS EndPage,
		[PPNR_PageNumberLocation] AS PageNumberLocation
	FROM
		[PdfPageNumberRenderer]
	WHERE
		[PPNR_PdfRendererBaseId] = @pdfRendererBaseId		
END