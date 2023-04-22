IF OBJECT_ID('[dbo].[s_GetSqlVariableConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetSqlVariableConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetSqlVariableConfig]
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