IF OBJECT_ID('[dbo].[s_PutPdfAnnotationRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfAnnotationRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfAnnotationRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@annotationRendererType TINYINT,
	@title NVARCHAR(MAX),
	@icon TINYINT,
	@content VARCHAR(MAX),
	@sqlTemplateConfigSqlConfigId UNIQUEIDENTIFIER,
	@sqlResColumn VARCHAR(50)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY	
		
		UPDATE 
			[dbo].[PdfAnnotationRenderer]
		SET
			[PAR_AnnotationRendererType] = @annotationRendererType,
			[PAR_Title] = @title,
			[PAR_Icon] = @icon,
			[PAR_Content] = @content,
			[PAR_SqlTemplateConfigSqlConfigId] = @sqlTemplateConfigSqlConfigId
		WHERE
			[PAR_PdfRendererBaseId] = @pdfRendererBaseId

		DELETE FROM [dbo].[SqlResColumnConfig]
		WHERE [SRCC_PdfRendererBaseId] = @pdfRendererBaseId
		
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