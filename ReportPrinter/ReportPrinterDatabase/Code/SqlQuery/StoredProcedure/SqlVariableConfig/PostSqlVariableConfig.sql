IF OBJECT_ID('PostSqlVariableConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PostSqlVariableConfig
END
GO

CREATE PROCEDURE PostSqlVariableConfig
	@sqlVariableConfigId UNIQUEIDENTIFIER,
	@sqlConfigId UNIQUEIDENTIFIER,
	@name VARCHAR(100)
AS
BEGIN
	INSERT INTO [dbo].[SqlVariableConfig] (
		[SVC_SqlVariableConfigId],
		[SVC_SqlConfigId],
		[SVC_Name]
	) VALUES (
		@sqlVariableConfigId,
		@sqlConfigId,
		@name
	)
END