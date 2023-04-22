IF OBJECT_ID('[dbo].[s_DeleteAllPdfRendererBase]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_DeleteAllPdfRendererBase]
END
GO

CREATE PROCEDURE [dbo].[s_DeleteAllPdfRendererBase]
AS
BEGIN	
	IF @@TRANCOUNT = 0 
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		DELETE FROM [dbo].[PdfRendererBase]

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH	
END