IF OBJECT_ID('[dbo].[s_GetAllSqlVariableConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlVariableConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlVariableConfig]
AS
BEGIN
	SELECT 
		[SVC_SqlVariableConfigId] AS SqlVariableConfigId,
		[SVC_SqlConfigId] AS SqlConfigId,
		[SVC_Name] AS Name
	FROM 
		[dbo].[SqlVariableConfig]
END