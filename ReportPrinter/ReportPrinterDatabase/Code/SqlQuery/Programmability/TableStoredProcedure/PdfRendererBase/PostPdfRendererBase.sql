IF OBJECT_ID('[dbo].[PostPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[PostPdfRendererBase]
	@pdfRendererBaseId UNIQUEIDENTIFIER,
	@id VARCHAR(50),
	@rendererType TINYINT,
	@row INT,
	@colum INT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		INSERT INTO [dbo].[PdfRendererBase] (
			[PRB_PdfRendererBaseId],
			[PRB_Id],
			[PRB_RendererType],
			[PRB_Row],
			[PRB_Column]
		) VALUES (
			@pdfRendererBaseId,
			@Id,
			@rendererType,
			@row,
			@colum
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END