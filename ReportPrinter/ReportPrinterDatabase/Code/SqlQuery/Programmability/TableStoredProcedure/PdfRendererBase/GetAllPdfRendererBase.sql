IF OBJECT_ID('[dbo].[GetAllPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[GetAllPdfRendererBase]
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
END