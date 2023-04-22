IF OBJECT_ID('[dbo].[s_PostPdfAnnotationRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfAnnotationRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfAnnotationRenderer]
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
		
		INSERT INTO [PdfAnnotationRenderer] (
			[PAR_PdfRendererBaseId],
			[PAR_AnnotationRendererType],
			[PAR_Title],
			[PAR_Icon],
			[PAR_Content],
			[PAR_SqlTemplateId],
			[PAR_SqlId],
			[PAR_SqlResColumn]
		) VALUES (
			@pdfRendererBaseId,
			@annotationRendererType,
			@title,
			@icon,
			@content,
			@sqlTemplateId,
			@sqlId,
			@sqlResColumn
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END