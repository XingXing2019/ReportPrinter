IF OBJECT_ID('[dbo].[s_PostPdfImageRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfImageRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfImageRenderer]
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
		
		INSERT INTO [PdfImageRenderer] (
			[PIR_PdfRendererBaseId],
			[PIR_SourceType],
			[PIR_ImageSource]
		) VALUES (
			@pdfRendererBaseId,
			@sourceType,
			@imageSource
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END