IF OBJECT_ID('[dbo].[s_PutPdfImageRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfImageRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfImageRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@sourceType TINYINT,
	@imageSource VARCHAR(MAX)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY	
		
		UPDATE 
			[PdfImageRenderer]
		SET
			[PIR_SourceType] = @sourceType,
			[PIR_ImageSource] = @imageSource
		WHERE
			[PIR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END