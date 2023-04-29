IF OBJECT_ID('[dbo].[s_GetAllSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlTemplateConfig]
AS
BEGIN
	SELECT 
		[STC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STC_Id] AS Id
	FROM 
		[dbo].[SqlTemplateConfig]
END