IF OBJECT_ID('[dbo].[s_PutPdfWaterMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfWaterMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfWaterMarkRenderer]
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
		
		UPDATE 
			[PdfWaterMarkRenderer]
		SET
			[PWMR_WaterMarkType] = @waterMarkType,
			[PWMR_Content] = @content,
			[PWMR_Location] = @location,
			[PWMR_SqlTemplateId] = @sqlTemplateId,
			[PWMR_SqlId] = @sqlId,
			[PWMR_SqlResColumn] = @sqlResColumn,
			[PWMR_StartPage] = @startPage,
			[PWMR_EndPage] = @endPage,
			[PWMR_Rotate] = @rotate
		WHERE
			[PWMR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END