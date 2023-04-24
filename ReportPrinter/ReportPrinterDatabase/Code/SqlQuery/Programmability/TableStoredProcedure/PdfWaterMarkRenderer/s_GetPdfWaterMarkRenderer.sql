IF OBJECT_ID('[dbo].[s_GetPdfWaterMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfWaterMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfWaterMarkRenderer]
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

		[PWMR_PdfWaterMarkRendererId] AS PdfWaterMarkRendererId,
		[PWMR_WaterMarkType] AS WaterMarkType,
		[PWMR_Content] AS Content,
		[PWMR_Location] AS Location,
		[PWMR_SqlTemplateId] AS SqlTemplateId,
		[PWMR_SqlId] AS SqlId,
		[PWMR_SqlResColumn] AS SqlResColumn,
		[PWMR_StartPage] AS StartPage,
		[PWMR_EndPage] AS EndPage,
		[PWMR_Rotate] AS Rotate
	FROM
		[PdfWaterMarkRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	WHERE
		[PWMR_PdfRendererBaseId] = @pdfRendererBaseId		
END