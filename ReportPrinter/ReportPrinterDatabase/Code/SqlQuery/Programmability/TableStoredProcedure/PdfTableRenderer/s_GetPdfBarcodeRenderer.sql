IF OBJECT_ID('[dbo].[s_GetPdfTableRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfTableRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfTableRenderer]
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
		[PdfRendererBaseId] AS PdfRendererBaseId,
		[Id] AS Id,
		[RendererType] AS RendererType,
		[Margin] AS Margin,
		[Padding] AS Padding,
		[HorizontalAlignment] AS HorizontalAlignment,
		[VerticalAlignment] AS VerticalAlignment,
		[Position] AS Position,
		[Left] AS [Left],
		[Right] AS [Right],
		[Top] AS [Top],
		[Bottom] AS Bottom,
		[FontSize] AS FontSize,
		[FontFamily] AS FontFamily,
		[FontStyle] AS FontStyle,
		[Opacity] AS Opacity,
		[BrushColor] AS BrushColor,
		[BackgroundColor] AS BackgroundColor,
		[Row] AS Row,
		[Column] AS [Column],
		[RowSpan] AS RowSpan,
		[ColumnSpan] AS ColumnSpan,

		[PTR_BoardThickness] AS BoardThickness,
		[PTR_LineSpace] AS LineSpace,
		[PTR_TitleHorizontalAlignment] TitleHorizontalAlignment,
		[PTR_HideTitle] HideTitle,
		[PTR_Space] Space,
		[PTR_TitleColor] TitleColor,
		[PTR_TitleColorOpacity] TitleColorOpacity,
		[PTR_SqlTemplateConfigSqlConfigId] SqlTemplateConfigSqlConfigId,
		[PTR_SqlVariable] SqlVariable,
		[PTR_SubPdfTableRendererId] SubPdfTableRendererId,
		[SqlTemplateId] AS SqlTemplateId,
		[SqlId] AS SqlId
	FROM
		[PdfTableRenderer]
	CROSS APPLY
		f_GetPdfRendererBase([PTR_PdfRendererBaseId])
	CROSS APPLY
		f_GetPdfRendererSqlInfo([PTR_SqlTemplateConfigSqlConfigId])
	WHERE
		[PTR_PdfRendererBaseId] IN (SELECT PdfRendererBaseId FROM @tableRenderers)
END