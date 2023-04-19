IF OBJECT_ID('[dbo].[PostPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[PostPdfRendererBase]
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
	
		INSERT INTO [PdfRendererBase] (
			[PRB_PdfRendererBaseId],
			[PRB_Id],
			[PRB_RendererType],
			[PRB_Margin],
			[PRB_Padding],
			[PRB_HorizontalAlignment],
			[PRB_VerticalAlignment],
			[PRB_Position],
			[PRB_Left],
			[PRB_Right],
			[PRB_Top],
			[PRB_Bottom],
			[PRB_FontSize],
			[PRB_FontFamily],
			[PRB_FontStyle],
			[PRB_Opacity],
			[PRB_BrushColor],
			[PRB_BackgroundColor],
			[PRB_Row],
			[PRB_Column],
			[PRB_RowSpan],
			[PRB_ColumnSpan]
		) VALUES (
			@pdfRendererBaseId,
			@id,
			@rendererType,
			@margin,
			@padding,
			@horizontalAlignment,
			@verticalAlignment,
			@position,
			@left,
			@right,
			@top,
			@bottom,
			@fontSize,
			@fontFamily,
			@fontStyle,
			@opacity,
			@brushColor,
			@backgroundColor,
			@row,
			@column,
			@rowSpan,
			@columnSpan
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END