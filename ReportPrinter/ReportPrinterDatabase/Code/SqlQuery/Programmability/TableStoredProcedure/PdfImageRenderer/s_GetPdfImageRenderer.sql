IF OBJECT_ID('[dbo].[s_GetPdfImageRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfImageRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfImageRenderer]
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

		[PIR_PdfImageRendererId] AS PdfImageRendererId,
		[PIR_SourceType] AS SourceType,
		[PIR_ImageSource] AS ImageSource
	FROM
		[PdfImageRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	WHERE
		[PIR_PdfRendererBaseId] = @pdfRendererBaseId		
END