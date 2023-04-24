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
		[PTR_SqlTemplateId] AS SqlTemplateId,
		[PTR_SqlId] AS SqlId,
		[PTR_SqlResColumn] AS SqlResColumn,
		[PTR_Mask] AS Mask,
		[PTR_Title] AS Title
	FROM
		[PdfTextRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	WHERE
		[PTR_PdfRendererBaseId] = @pdfRendererBaseId		
END