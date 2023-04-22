IF OBJECT_ID('[dbo].[s_PutPdfTextRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfTextRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfTextRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,		
	@textRendererType TINYINT,
	@content VARCHAR(200),
	@sqlTemplateId VARCHAR(50),
	@sqlId VARCHAR(50),
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
		
		UPDATE 
			[PdfTextRenderer]
		SET
			[PTR_TextRendererType] = @textRendererType,
			[PTR_Content] = @content,
			[PTR_SqlTemplateId] = @sqlTemplateId,
			[PTR_SqlId] = @sqlId,
			[PTR_SqlResColumn] = @sqlResColumn,
			[PTR_Mask] = @mask,
			[PTR_Title] = @title
		WHERE
			[PTR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END