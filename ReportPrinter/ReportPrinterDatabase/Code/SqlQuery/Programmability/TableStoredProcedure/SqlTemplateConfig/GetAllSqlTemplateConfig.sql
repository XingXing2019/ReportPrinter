IF OBJECT_ID('[dbo].[GetAllSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[GetAllSqlTemplateConfig]
AS
BEGIN
	SELECT 
		[STC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STC_Id] AS Id
	FROM 
		[dbo].[SqlTemplateConfig]
END