IF OBJECT_ID('[dbo].[GetPdfImageRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfImageRenderer]
END
GO

CREATE PROCEDURE [dbo].[GetPdfImageRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT
		[PIR_PdfImageRendererId] AS PdfImageRendererId,
		[PIR_PdfRendererBaseId] AS PdfRendererBaseId,
		[PIR_SourceType] AS SourceType,
		[PIR_ImageSource] AS ImageSource
	FROM
		[PdfImageRenderer]
	WHERE
		[PIR_PdfRendererBaseId] = @pdfRendererBaseId		
END