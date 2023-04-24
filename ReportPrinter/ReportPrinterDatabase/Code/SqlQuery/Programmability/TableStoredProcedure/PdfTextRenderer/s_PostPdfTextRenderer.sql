IF OBJECT_ID('[dbo].[s_PostPdfTextRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfTextRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfTextRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,		
	@textRendererType TINYINT,
	@content VARCHAR(200),
	@sqlTemplateConfigSqlConfigId UNIQUEIDENTIFIER,
	@sqlResColumn VARCHAR(50), 
	@mask VARCHAR(50),
	@title VARCHAR(50)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
		
		INSERT INTO [PdfTextRenderer] (			
			[PTR_PdfRendererBaseId],
			[PTR_TextRendererType],
			[PTR_Content],
			[PTR_SqlTemplateConfigSqlConfigId],
			[PTR_Mask],
			[PTR_Title]
		) VALUES (
			@pdfRendererBaseId,
			@TextRendererType,
			@Content,
			@sqlTemplateConfigSqlConfigId,
			@Mask,
			@Title
		)

		IF @sqlResColumn IS NOT NULL 
		BEGIN
			INSERT INTO [dbo].[SqlResColumnConfig] (
				[SRCC_PdfRendererBaseId],
				[SRCC_Name]
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