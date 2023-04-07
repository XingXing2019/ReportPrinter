IF OBJECT_ID('DeleteSqlConfigById', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteSqlConfigById
END
GO

CREATE PROCEDURE DeleteSqlConfigById
	@sqlConfigId UNIQUEIDENTIFIER
AS
BEGIN
	
	IF @@TRANCOUNT = 0 
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		DELETE FROM [dbo].[SqlConfig]
		WHERE [SC_SqlConfigId] = @sqlConfigId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
	
END