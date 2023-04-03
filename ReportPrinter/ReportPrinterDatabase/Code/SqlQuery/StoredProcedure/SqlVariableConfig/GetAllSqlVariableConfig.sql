IF OBJECT_ID('GetAllSqlVariableConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE GetAllSqlVariableConfig
END
GO

CREATE PROCEDURE GetAllSqlVariableConfig
AS
BEGIN
	SELECT 
		[SVC_SqlVariableConfigId] AS SqlVariableConfigId,
		[SVC_SqlConfigId] AS SqlConfigId,
		[SVC_Name] AS Name
	FROM 
		[dbo].[SqlVariableConfig]
END