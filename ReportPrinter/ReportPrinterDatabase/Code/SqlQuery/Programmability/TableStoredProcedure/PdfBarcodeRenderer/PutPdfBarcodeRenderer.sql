IF OBJECT_ID('[dbo].[PutPdfAnnotationRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PutPdfAnnotationRenderer]
END
GO

CREATE PROCEDURE [dbo].[PutPdfAnnotationRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@annotationRendererType TINYINT,
	@title NVARCHAR(MAX),
	@icon TINYINT,
	@content VARCHAR(MAX),
	@sqlTemplateId VARCHAR(50),
	@sqlId VARCHAR(50),
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
			[PdfAnnotationRenderer]
		SET
			[PAR_AnnotationRendererType] = @annotationRendererType,
			[PAR_Title] = @title,
			[PAR_Icon] = @icon,
			[PAR_Content] = @content,
			[PAR_SqlTemplateId] = @sqlTemplateId,
			[PAR_SqlId] = @sqlId,
			[PAR_SqlResColumn] = @sqlResColumn
		WHERE
			[PAR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END