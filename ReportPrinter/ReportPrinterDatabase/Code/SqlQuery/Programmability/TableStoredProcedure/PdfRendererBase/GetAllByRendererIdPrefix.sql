IF OBJECT_ID('[dbo].[GetAllByRendererIdPrefix]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllByRendererIdPrefix]
END
GO

CREATE PROCEDURE [dbo].[GetAllByRendererIdPrefix]
	@rendererIdPrefix VARCHAR(50)
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
		[PRB_Id] LIKE @rendererIdPrefix + '%'
	ORDER BY
		[PRB_Id]
END