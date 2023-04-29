IF OBJECT_ID('[dbo].[s_PutPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfRendererBase]
	@pdfRendererBaseId UNIQUEIDENTIFIER,
	@id VARCHAR(50),
	@rendererType TINYINT,
	@margin VARCHAR(20),
	@padding VARCHAR(20),
	@horizontalAlignment TINYINT,
	@verticalAlignment TINYINT,
	@position TINYINT,
	@left FLOAT,
	@right FLOAT,
	@top FLOAT,
	@bottom FLOAT,
	@fontSize FLOAT,
	@fontFamily VARCHAR(50),
	@fontStyle TINYINT,
	@opacity FLOAT,
	@brushColor TINYINT,
	@backgroundColor TINYINT,
	@row INT,
	@column INT,
	@rowSpan INT,
	@columnSpan INT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		UPDATE 
			[PdfRendererBase]
		SET
			[PRB_Id] = @id,
			[PRB_RendererType] = @rendererType,
			[PRB_Margin] = @margin,
			[PRB_Padding] = @padding,
			[PRB_HorizontalAlignment] = @horizontalAlignment,
			[PRB_VerticalAlignment] = @verticalAlignment,
			[PRB_Position] = @position,
			[PRB_Left] = @left,
			[PRB_Right] = @right,
			[PRB_Top] = @top,
			[PRB_Bottom] = @bottom,
			[PRB_FontSize] = @fontSize,
			[PRB_FontFamily] = @fontFamily,
			[PRB_FontStyle] = @fontStyle,
			[PRB_Opacity] = @opacity,
			[PRB_BrushColor] = @brushColor,
			[PRB_BackgroundColor] = @backgroundColor,
			[PRB_Row] = @row,
			[PRB_Column] = @column,
			[PRB_RowSpan] = @rowSpan,
			[PRB_ColumnSpan] = @columnSpan

		WHERE
			[PRB_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END