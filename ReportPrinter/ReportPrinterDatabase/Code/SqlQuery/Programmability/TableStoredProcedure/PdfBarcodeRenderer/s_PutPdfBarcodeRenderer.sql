IF OBJECT_ID('[dbo].[s_PutPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfBarcodeRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@barcodeFormat INT,
	@showBarcodeText BIT,
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
			[PdfBarcodeRenderer]
		SET
			[PBR_BarcodeFormat] = @barcodeFormat,
			[PBR_ShowBarcodeText] = @showBarcodeText,
			[PBR_SqlTemplateConfigSqlConfigId] = @sqlTemplateConfigSqlConfigId
		WHERE
			[PBR_PdfRendererBaseId] = @pdfRendererBaseId

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