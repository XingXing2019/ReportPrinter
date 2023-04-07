IF OBJECT_ID('DeleteAllSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteAllSqlConfig
END
GO

CREATE PROCEDURE DeleteAllSqlConfig
AS
BEGIN
	
	SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	BEGIN TRANSACTION

	BEGIN TRY
	
		DELETE FROM [dbo].[SqlConfig]

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
	
END