IF OBJECT_ID('[dbo].[GetSqlVariableConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetSqlVariableConfig]
END
GO

CREATE PROCEDURE [dbo].[GetSqlVariableConfig]
	@sqlConfigId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[SVC_SqlVariableConfigId] AS SqlVariableConfigId,
		[SVC_SqlConfigId] AS SqlConfigId,
		[SVC_Name] AS Name
	FROM 
		[dbo].[SqlVariableConfig]
	WHERE
		[SVC_SqlConfigId] = @sqlConfigId
END