IF OBJECT_ID('[dbo].[s_PostPdfPageNumberRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfPageNumberRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfPageNumberRenderer]
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
		
		INSERT INTO [PdfPageNumberRenderer] (
			[PPNR_PdfRendererBaseId],
			[PPNR_StartPage],
			[PPNR_EndPage],
			[PPNR_PageNumberLocation]
		) VALUES (
			@pdfRendererBaseId,
			@startPage,
			@endPage,
			@pageNumberLocation
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END