IF OBJECT_ID('[dbo].[f_GetPdfRendererBase]', 'TF') IS NOT NULL
BEGIN
	DROP FUNCTION [dbo].[f_GetPdfRendererBase]
END
GO

CREATE FUNCTION [dbo].[f_GetPdfRendererBase] (
	@pdfRendererId UNIQUEIDENTIFIER
)
RETURNS @Result TABLE (
	[PRB_PdfRendererBaseId] UNIQUEIDENTIFIER,
	[PRB_Id] VARCHAR(50) NOT NULL,
	[PRB_RendererType] TINYINT NOT NULL,
	[PRB_Margin] VARCHAR(20) NULL,
	[PRB_Padding] VARCHAR(20) NULL,
	[PRB_HorizontalAlignment] TINYINT NULL,
	[PRB_VerticalAlignment] TINYINT NULL,
	[PRB_Position] TINYINT NULL,
	[PRB_Left] FLOAT NULL,
	[PRB_Right] FLOAT NULL,
	[PRB_Top] FLOAT NULL,
	[PRB_Bottom] FLOAT NULL,
	[PRB_FontSize] FLOAT NULL,
	[PRB_FontFamily] VARCHAR(50) NULL,
	[PRB_FontStyle] TINYINT NULL,
	[PRB_Opacity] FLOAT NULL,
	[PRB_BrushColor] TINYINT NULL,
	[PRB_BackgroundColor] TINYINT NULL,
	[PRB_Row] INT NOT NULL,
	[PRB_Column] INT NOT NULL,
	[PRB_RowSpan] INT NULL,
	[PRB_ColumnSpan] INT NULL
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