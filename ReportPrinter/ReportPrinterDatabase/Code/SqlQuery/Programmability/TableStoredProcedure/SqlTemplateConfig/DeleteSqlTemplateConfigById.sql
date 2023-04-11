IF OBJECT_ID('[dbo].[DeleteSqlTemplateConfigById]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[DeleteSqlTemplateConfigById]
END
GO

CREATE PROCEDURE [dbo].[DeleteSqlTemplateConfigById]
	@sqlTemplateConfigId UNIQUEIDENTIFIER
AS
BEGIN	
	IF @@TRANCOUNT = 0 
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		DELETE FROM [dbo].[SqlTemplateConfig]
		WHERE [STC_SqlTemplateConfigId] = @sqlTemplateConfigId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH	
END