IF OBJECT_ID('[dbo].[PostPdfImageRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostPdfImageRenderer]
END
GO

CREATE PROCEDURE [dbo].[PostPdfImageRenderer]
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
		
		-- Insert PdfBarcodeRenderer
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