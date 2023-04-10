IF OBJECT_ID('[dbo].[PutSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PutSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[PutSqlTemplateConfig]
	@sqlTemplateConfigId UNIQUEIDENTIFIER,
	@id VARCHAR(50),
	@sqlConfigIds NVARCHAR(MAX)
AS
BEGIN	
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	IF EXISTS (SELECT * FROM SqlTemplateConfig WHERE STC_SqlTemplateConfigId = @sqlTemplateConfigId)
	BEGIN
		BEGIN TRANSACTION

		BEGIN TRY
		
			-- Update SqlTemplateConfig table
			UPDATE [dbo].[SqlTemplateConfig]
			SET
				[STC_Id] = @id
			WHERE
				[STC_SqlTemplateConfigId] = @sqlTemplateConfigId

			-- Delete SqlTemplateConfigSqlConfig table
			DELETE FROM [dbo].[SqlTemplateConfigSqlConfig]
			WHERE [STCSC_SqlTemplateConfigId] = @sqlTemplateConfigId

			-- Insert new SqlTemplateConfigSqlConfig
			DECLARE @sqlConfigId NVARCHAR(100)
			DECLARE @temp TABLE (
				SqlConfigId UNIQUEIDENTIFIER
			);

			IF @sqlConfigIds <> ''
			BEGIN
				INSERT INTO @temp
				SELECT value FROM STRING_SPLIT(@sqlConfigIds, ',')
			END
			
			DECLARE idCursor CURSOR
			FOR SELECT SqlConfigId FROM @temp

			OPEN idCursor
			
			FETCH NEXT FROM idCursor INTO @sqlConfigId
			
			WHILE @@FETCH_STATUS = 0
			BEGIN
				INSERT INTO [dbo].[SqlTemplateConfigSqlConfig] (
					[STCSC_SqlTemplateConfigId],
					[STCSC_SqlConfigId]
				) VALUES (
					@sqlTemplateConfigId,
					@sqlConfigId
				)
				
				FETCH NEXT FROM idCursor INTO @sqlConfigId
			END

			COMMIT TRANSACTION

		END TRY

		BEGIN CATCH
			
			ROLLBACK TRANSACTION
			DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
			RAISERROR(@errorMsg, 16, 1)

		END CATCH
	END
END