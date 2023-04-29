IF OBJECT_ID('[dbo].[s_DeletePdfRendererBaseByIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_DeletePdfRendererBaseByIds]
END
GO

CREATE PROCEDURE [dbo].[s_DeletePdfRendererBaseByIds]
	@pdfRendererBaseIds NVARCHAR(MAX)
AS
BEGIN	
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		DECLARE @temp TABLE (
			PdfRendererBaseId UNIQUEIDENTIFIER
		);
		
		IF @pdfRendererBaseIds <> ''
		BEGIN
			INSERT INTO @temp
			SELECT value FROM STRING_SPLIT(@pdfRendererBaseIds, ',')
		END

		DELETE FROM [PdfRendererBase]
		WHERE [PRB_PdfRendererBaseId] IN (
			SELECT * FROM @temp
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END