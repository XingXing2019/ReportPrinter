IF OBJECT_ID('[dbo].[s_GetPdfPageNumberRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfPageNumberRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfPageNumberRenderer]
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

		[PPNR_PdfPageNumberRendererId] AS PdfPageNumberRendererId,
		[PPNR_StartPage] AS StartPage,
		[PPNR_EndPage] AS EndPage,
		[PPNR_PageNumberLocation] AS PageNumberLocation
	FROM
		[PdfPageNumberRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	WHERE
		[PPNR_PdfRendererBaseId] = @pdfRendererBaseId		
END