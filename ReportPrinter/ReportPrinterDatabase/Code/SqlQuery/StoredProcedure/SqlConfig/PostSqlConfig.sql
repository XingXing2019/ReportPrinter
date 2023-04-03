IF OBJECT_ID('PostSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PostSqlConfig
END
GO

CREATE PROCEDURE PostSqlConfig
	@sqlConfigId UNIQUEIDENTIFIER,
	@id VARCHAR(100),
	@databaseId VARCHAR(100),
	@query NVARCHAR(MAX)
AS
BEGIN
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
END