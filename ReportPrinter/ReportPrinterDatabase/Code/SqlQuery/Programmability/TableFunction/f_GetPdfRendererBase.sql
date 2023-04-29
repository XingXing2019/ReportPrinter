IF OBJECT_ID('[dbo].[f_GetPdfRendererBase]', 'TF') IS NOT NULL
BEGIN
	DROP FUNCTION [dbo].[f_GetPdfRendererBase]
END
GO

CREATE FUNCTION [dbo].[f_GetPdfRendererBase] (
	@pdfRendererId UNIQUEIDENTIFIER
)
RETURNS @Result TABLE (
	[PdfRendererBaseId] UNIQUEIDENTIFIER,
	[Id] VARCHAR(50) NOT NULL,
	[RendererType] TINYINT NOT NULL,
	[Margin] VARCHAR(20) NULL,
	[Padding] VARCHAR(20) NULL,
	[HorizontalAlignment] TINYINT NULL,
	[VerticalAlignment] TINYINT NULL,
	[Position] TINYINT NULL,
	[Left] FLOAT NULL,
	[Right] FLOAT NULL,
	[Top] FLOAT NULL,
	[Bottom] FLOAT NULL,
	[FontSize] FLOAT NULL,
	[FontFamily] VARCHAR(50) NULL,
	[FontStyle] TINYINT NULL,
	[Opacity] FLOAT NULL,
	[BrushColor] TINYINT NULL,
	[BackgroundColor] TINYINT NULL,
	[Row] INT NOT NULL,
	[Column] INT NOT NULL,
	[RowSpan] INT NULL,
	[ColumnSpan] INT NULL
)
AS
BEGIN
	INSERT INTO
		@Result
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
		[PRB_ColumnSpan] AS ColumnSpan
 	FROM 
		[dbo].[PdfRendererBase]
	WHERE 
		[PRB_PdfRendererBaseId] = @pdfRendererId

	RETURN
END