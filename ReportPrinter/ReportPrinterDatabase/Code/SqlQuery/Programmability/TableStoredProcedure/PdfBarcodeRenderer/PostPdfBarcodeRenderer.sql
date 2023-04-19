IF OBJECT_ID('[dbo].[PostPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[PostPdfBarcodeRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@barcodeFormat INT,
	@showBarcodeText BIT,
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
		
		-- Insert PdfBarcodeRenderer
		INSERT INTO [PdfBarcodeRenderer] (
			[PBR_PdfRendererBaseId],
			[PBR_BarcodeFormat],
			[PBR_ShowBarcodeText],
			[PBR_SqlTemplateId],
			[PBR_SqlId],
			[PBR_SqlResColumn]
		) VALUES (
			@pdfRendererBaseId,
			@barcodeFormat,
			@showBarcodeText,
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