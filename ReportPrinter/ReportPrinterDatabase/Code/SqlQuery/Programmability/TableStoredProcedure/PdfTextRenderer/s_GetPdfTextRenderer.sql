IF OBJECT_ID('[dbo].[s_GetPdfTextRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfTextRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfTextRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
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

		[PTR_PdfTextRendererId] AS PdfTextRendererId,
		[PTR_TextRendererType] AS TextRendererType,
		[PTR_Content] AS Content,		
		[PTR_Mask] AS Mask,
		[PTR_Title] AS Title,

		[PTR_SqlTemplateConfigSqlConfigId] AS SqlTemplateConfigSqlConfigId,
		[SqlTemplateId] AS SqlTemplateId,
		[SqlId] AS SqlId,
		[SRCC_Name] AS SqlResColumn
	FROM
		[PdfTextRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	OUTER APPLY
		f_GetPdfRendererSqlInfo([PTR_SqlTemplateConfigSqlConfigId])
	LEFT JOIN
		[dbo].[SqlResColumnConfig] ON [SRCC_PdfRendererBaseId] = [PTR_PdfRendererBaseId]
	WHERE
		[PTR_PdfRendererBaseId] = @pdfRendererBaseId		
END