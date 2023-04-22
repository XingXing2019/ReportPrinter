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
		[PRB_PdfRendererBaseId] AS PdfRendererBaseId,
		[PRB_Id] AS Id,
		[PRB_RendererType] AS RendererType,
		[PRB_Margin] AS Margin,
		[PRB_Padding] AS Padding,
		[PRB_HorizontalAlignment] AS HorizontalAlignment,
		[PRB_VerticalAlignment] AS VerticalAlignment,
		[PRB_Position] AS Position,
		[PRB_Left] AS [Left],
		[PRB_Right] AS [Right],
		[PRB_Top] AS [Top],
		[PRB_Bottom] AS Bottom,
		[PRB_FontSize] AS FontSize,
		[PRB_FontFamily] AS FontFamily,
		[PRB_FontStyle] AS FontStyle,
		[PRB_Opacity] AS Opacity,
		[PRB_BrushColor] AS BrushColor,
		[PRB_BackgroundColor] AS BackgroundColor,
		[PRB_Row] AS Row,
		[PRB_Column] AS [Column],
		[PRB_RowSpan] AS RowSpan,
		[PRB_ColumnSpan] AS ColumnSpan,

		[PAR_PdfAnnotationRendererId] AS PdfAnnotationRendererId,
		[PAR_AnnotationRendererType] AS AnnotationRendererType,
		[PAR_Title] AS Title,
		[PAR_Icon] AS Icon,
		[PAR_Content] AS Content,
		[PAR_SqlTemplateId] AS SqlTemplateId,
		[PAR_SqlId] AS SqlId,
		[PAR_SqlResColumn] AS SqlResColumn
	FROM
		[PdfAnnotationRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	WHERE
		[PAR_PdfRendererBaseId] = @pdfRendererBaseId		
END