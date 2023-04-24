IF OBJECT_ID('[dbo].[s_PostPdfBarcodeRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfBarcodeRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfBarcodeRenderer]
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
		
		INSERT INTO [PdfBarcodeRenderer] (
			[PBR_PdfRendererBaseId],
			[PBR_BarcodeFormat],
			[PBR_ShowBarcodeText],
			[PBR_SqlTemplateConfigSqlConfigId]
		) VALUES (
			@pdfRendererBaseId,
			@barcodeFormat,
			@showBarcodeText,
			@sqlTemplateConfigSqlConfigId
		)

		IF @sqlResColumn IS NOT NULL 
		BEGIN
			INSERT INTO [dbo].[SqlResColumnConfig] (
				[SRCC_PdfRendererBaseId],
				[SRCC_Name]
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