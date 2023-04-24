IF OBJECT_ID('[dbo].[s_GetPdfAnnotationRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfAnnotationRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfAnnotationRenderer]
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

		[PAR_PdfAnnotationRendererId] AS PdfAnnotationRendererId,
		[PAR_AnnotationRendererType] AS AnnotationRendererType,
		[PAR_Title] AS Title,
		[PAR_Icon] AS Icon,
		[PAR_Content] AS Content,
		[PAR_SqlTemplateConfigSqlConfigId] AS SqlTemplateConfigSqlConfigId,
		[SqlTemplateId] AS SqlTemplateId,
		[SqlId] AS SqlId,
		[SRCC_Name] AS SqlResColumn
	FROM
		[dbo].[PdfAnnotationRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	OUTER APPLY
		f_GetPdfRendererSqlInfo([PAR_SqlTemplateConfigSqlConfigId])
	LEFT JOIN
		[dbo].[SqlResColumnConfig] ON [SRCC_PdfRendererBaseId] = [PAR_PdfRendererBaseId]
	WHERE
		[PAR_PdfRendererBaseId] = @pdfRendererBaseId		
END