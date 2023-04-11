IF OBJECT_ID('[dbo].[GetAllSqlTemplateConfigSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllSqlTemplateConfigSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[GetAllSqlTemplateConfigSqlConfig]
AS
BEGIN
	SELECT 
		[STCSC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STCSC_SqlConfigId] AS SqlConfigId
	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
END