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