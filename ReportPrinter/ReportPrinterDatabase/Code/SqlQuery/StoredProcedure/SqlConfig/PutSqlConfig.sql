IF OBJECT_ID('PutSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PutSqlConfig
END
GO

CREATE PROCEDURE PutSqlConfig
	@sqlConfigId UNIQUEIDENTIFIER,
	@id VARCHAR(100),
	@databaseId VARCHAR(100),
	@query NVARCHAR(MAX),
	@sqlVariableNames NVARCHAR(MAX)
AS
BEGIN
	
	SET TRANSACTION ISOLATION LEVEL SNAPSHOT

	IF EXISTS (SELECT * FROM SqlConfig WHERE SC_SqlConfigId = @sqlConfigId)
	BEGIN
		BEGIN TRANSACTION

		BEGIN TRY
		
			-- Update SqlConfig table
			UPDATE [dbo].[SqlConfig]
			SET
				[SC_Id] = @id,
				[SC_DatabaseId] = @databaseId,
				[SC_Query] = @query
			WHERE
				[SC_SqlConfigId] = @sqlConfigId

			-- Delete SqlConfigVariables table
			DELETE FROM [dbo].[SqlVariableConfig]
			WHERE [SVC_SqlConfigId] = @sqlConfigId

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
END