IF OBJECT_ID('[dbo].[PutPdfPageNumberRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PutPdfPageNumberRenderer]
END
GO

CREATE PROCEDURE [dbo].[PutPdfPageNumberRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,	
	@startPage INT,
	@endPage INT,
	@pageNumberLocation TINYINT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY	
		
		UPDATE 
			[PdfPageNumberRenderer]
		SET
			[PPNR_StartPage] = @startPage,
			[PPNR_EndPage] = @endPage,
			[PPNR_PageNumberLocation] = @pageNumberLocation
		WHERE
			[PPNR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END