IF OBJECT_ID('[dbo].[GetAllByRendererType]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllByRendererType]
END
GO

CREATE PROCEDURE [dbo].[GetAllByRendererType]
	@rendererType TINYINT
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
		[PRB_RendererType] = @rendererType
END