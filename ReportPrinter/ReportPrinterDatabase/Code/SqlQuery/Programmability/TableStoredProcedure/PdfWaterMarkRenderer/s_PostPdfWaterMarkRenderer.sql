IF OBJECT_ID('[dbo].[s_PostPdfWaterMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfWaterMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfWaterMarkRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,
	@waterMarkType TINYINT,
	@content VARCHAR(200),
	@location TINYINT,
	@sqlTemplateId VARCHAR(50),
	@sqlId VARCHAR(50),
	@sqlResColumn VARCHAR(50),
	@startPage INT,
	@endPage INT,
	@rotate FLOAT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
		
		INSERT INTO [PdfWaterMarkRenderer] (			
			[PWMR_PdfRendererBaseId],
			[PWMR_WaterMarkType],
			[PWMR_Content],
			[PWMR_Location],
			[PWMR_SqlTemplateId],
			[PWMR_SqlId],
			[PWMR_SqlResColumn],
			[PWMR_StartPage],
			[PWMR_EndPage],
			[PWMR_Rotate]
		) VALUES (
			@pdfRendererBaseId,
			@waterMarkType,
			@content,
			@location,
			@sqlTemplateId,
			@sqlId,
			@sqlResColumn,
			@startPage,
			@endPage,
			@rotate
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END