IF OBJECT_ID('[dbo].[s_PostSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[s_PostSqlTemplateConfig]
	@sqlTemplateConfigId UNIQUEIDENTIFIER,
	@id VARCHAR(50),
	@sqlConfigIds NVARCHAR(MAX)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		-- Insert SqlTemplateConfig table
		INSERT INTO [dbo].[SqlTemplateConfig] (
			[STC_SqlTemplateConfigId],
			[STC_Id]
		) VALUES (
			@sqlTemplateConfigId,
			@Id
		)

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