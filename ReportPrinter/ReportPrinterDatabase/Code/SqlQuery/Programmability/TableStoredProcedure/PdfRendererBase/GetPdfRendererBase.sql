IF OBJECT_ID('[dbo].[GetPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[GetPdfRendererBase]
	@pdfRendererId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[PRB_PdfRendererBaseId] AS PdfRendererBaseId,
		[PRB_Id] AS Id,
		[PRB_RendererType] AS RendererType,
		[PRB_Row] AS Row,
		[PRB_Column] AS [Column]
 	FROM 
		[dbo].[PdfRendererBase]
	WHERE 
		[PRB_PdfRendererBaseId] = @pdfRendererId
END