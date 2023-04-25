IF OBJECT_ID('[dbo].[s_GetPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_GetPdfBarcodeRenderer]
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

		[PBR_PdfBarcodeRendererId] AS PdfBarcodeRendererId,
		[PBR_BarcodeFormat] AS BarcodeFormat,
		[PBR_ShowBarcodeText] AS ShowBarcodeText,

		[PBR_SqlTemplateConfigSqlConfigId] AS SqlTemplateConfigSqlConfigId,
		[SqlTemplateId] AS SqlTemplateId,
		[SqlId] AS SqlId,
		[SRCC_Id] AS SqlResColumn
	FROM
		[PdfBarcodeRenderer]
	CROSS APPLY
		f_GetPdfRendererBase(@pdfRendererBaseId)
	OUTER APPLY
		f_GetPdfRendererSqlInfo([PBR_SqlTemplateConfigSqlConfigId])
	LEFT JOIN
		[dbo].[SqlResColumnConfig] ON [SRCC_PdfRendererBaseId] = [PBR_PdfRendererBaseId]
	WHERE
		[PBR_PdfRendererBaseId] = @pdfRendererBaseId		
END