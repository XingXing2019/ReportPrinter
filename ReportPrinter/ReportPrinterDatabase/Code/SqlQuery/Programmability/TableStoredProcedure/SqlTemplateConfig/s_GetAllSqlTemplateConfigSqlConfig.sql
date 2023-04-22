IF OBJECT_ID('[dbo].[s_GetAllSqlTemplateConfigSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlTemplateConfigSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlTemplateConfigSqlConfig]
AS
BEGIN
	SELECT 
		[STCSC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STCSC_SqlConfigId] AS SqlConfigId
	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
END