IF OBJECT_ID('[dbo].[s_DeleteSqlTemplateConfigByIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_DeleteSqlTemplateConfigByIds]
END
GO

CREATE PROCEDURE [dbo].[s_DeleteSqlTemplateConfigByIds]
	@sqlTemplateConfigIds NVARCHAR(MAX)
AS
BEGIN	
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		DECLARE @temp TABLE (
			SqlTemplateConfigId UNIQUEIDENTIFIER
		);
		
		IF @sqlTemplateConfigIds <> ''
		BEGIN
			INSERT INTO @temp
			SELECT value FROM STRING_SPLIT(@sqlTemplateConfigIds, ',')
		END

		DELETE FROM SqlTemplateConfig
		WHERE STC_SqlTemplateConfigId IN (
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