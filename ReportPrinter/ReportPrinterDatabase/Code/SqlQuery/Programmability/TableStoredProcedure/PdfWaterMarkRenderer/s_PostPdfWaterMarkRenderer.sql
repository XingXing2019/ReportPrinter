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
	@sqlTemplateConfigSqlConfigId UNIQUEIDENTIFIER,
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
			[PWMR_SqlTemplateConfigSqlConfigId],
			[PWMR_StartPage],
			[PWMR_EndPage],
			[PWMR_Rotate]
		) VALUES (
			@pdfRendererBaseId,
			@waterMarkType,
			@content,
			@location,
			@sqlTemplateConfigSqlConfigId,
			@startPage,
			@endPage,
			@rotate
		)

		IF @sqlResColumn IS NOT NULL 
		BEGIN
			INSERT INTO [dbo].[SqlResColumnConfig] (
				[SRCC_PdfRendererBaseId],
				[SRCC_Id]
			) VALUES (
				@pdfRendererBaseId,
				@sqlResColumn
			)
		END

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END