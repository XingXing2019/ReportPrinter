IF OBJECT_ID('[dbo].[s_GetSqlResColumnConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetSqlResColumnConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetSqlResColumnConfig]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @tableRenderers TABLE 
	(
		PdfRendererBaseId UNIQUEIDENTIFIER
	)

	WHILE @pdfRendererBaseId IS NOT NULL
	BEGIN
		INSERT INTO @tableRenderers
		VALUES (@pdfRendererBaseId)
	
		SET @pdfRendererBaseId = (
			SELECT [PTR_SubPdfTableRendererId] FROM [PdfTableRenderer] 
			WHERE [PTR_PdfRendererBaseId] = @pdfRendererBaseId
		)
	END
		
	SELECT
		[SRCC_PdfRendererBaseId] AS PdfRendererBaseId,
		[SRCC_Id] AS Id,
		[SRCC_Title] AS Title,
		[SRCC_WidthRatio] AS WidthRatio,
		[SRCC_Position] AS Position,
		[SRCC_Left] AS [Left],
		[SRCC_Right] AS [Right]
	FROM
		[SqlResColumnConfig]
	WHERE
		[SRCC_PdfRendererBaseId] IN (SELECT PdfRendererBaseId FROM @tableRenderers)
		
END