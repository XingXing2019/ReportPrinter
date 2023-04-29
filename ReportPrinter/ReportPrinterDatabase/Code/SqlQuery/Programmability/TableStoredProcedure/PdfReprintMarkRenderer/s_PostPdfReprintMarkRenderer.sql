IF OBJECT_ID('[dbo].[s_PostPdfReprintMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfReprintMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfReprintMarkRenderer]
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
		
		INSERT INTO [PdfReprintMarkRenderer] (
			[PRMR_PdfRendererBaseId],
			[PRMR_Text],
			[PRMR_BoardThickness],
			[PRMR_Location]
		) VALUES (
			@pdfRendererBaseId,
			@text,
			@boardThickness,
			@location
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END