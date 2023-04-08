IF OBJECT_ID('[dbo].[GetAllSqlVariableConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllSqlVariableConfig]
END
GO

CREATE PROCEDURE [dbo].[GetAllSqlVariableConfig]
AS
BEGIN
	SELECT 
		[SVC_SqlVariableConfigId] AS SqlVariableConfigId,
		[SVC_SqlConfigId] AS SqlConfigId,
		[SVC_Name] AS Name
	FROM 
		[dbo].[SqlVariableConfig]
END