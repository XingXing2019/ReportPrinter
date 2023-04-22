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