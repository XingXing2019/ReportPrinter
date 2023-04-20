IF OBJECT_ID('[dbo].[PutPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PutPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[PutPdfBarcodeRenderer]
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
		
		UPDATE 
			[PdfBarcodeRenderer]
		SET
			[PBR_BarcodeFormat] = @barcodeFormat,
			[PBR_ShowBarcodeText] = @showBarcodeText,
			[PBR_SqlTemplateId] = @sqlTemplateId,
			[PBR_SqlId] = @sqlId,
			[PBR_SqlResColumn] = @sqlResColumn
		WHERE
			[PBR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END