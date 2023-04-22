IF OBJECT_ID('[dbo].[PutPdfReprintMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PutPdfReprintMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[PutPdfReprintMarkRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@text VARCHAR(100),
	@boardThickness FLOAT,
	@location TINYINT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY	
		
		UPDATE 
			[PdfReprintMarkRenderer]
		SET
			[PRMR_Text] = @text,
			[PRMR_BoardThickness] = @boardThickness,
			[PRMR_Location] = @location
		WHERE
			[PRMR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END