IF OBJECT_ID('[dbo].[PostSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[PostSqlConfig]
	@sqlConfigId UNIQUEIDENTIFIER,
	@id VARCHAR(100),
	@databaseId VARCHAR(100),
	@query NVARCHAR(MAX),
	@sqlVariableNames NVARCHAR(MAX)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
	
		-- Insert SqlConfig table
		INSERT INTO [dbo].[SqlConfig] (
			[SC_SqlConfigId],
			[SC_Id],
			[SC_DatabaseId],
			[SC_Query]
		) VALUES (
			@sqlConfigId,
			@Id,
			@databaseId, 
			@query
		)

		-- Insert new SqlConfigVariables
		DECLARE @sqlVariableName NVARCHAR(100)
		DECLARE @temp TABLE (
			SqlVariableName NVARCHAR(100)
		);
		
		INSERT INTO @temp
		SELECT value FROM STRING_SPLIT(@sqlVariableNames, ','); 

		DECLARE nameCursor CURSOR
		FOR SELECT SqlVariableName FROM @temp

		OPEN nameCursor
		
		FETCH NEXT FROM nameCursor INTO @sqlVariableName
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			INSERT INTO [dbo].[SqlVariableConfig] (
				[SVC_SqlConfigId],
				[SVC_Name]
			) VALUES (
				@sqlConfigId,
				@sqlVariableName
			)
			
			FETCH NEXT FROM nameCursor INTO @sqlVariableName
		END

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END