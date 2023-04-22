IF OBJECT_ID('[dbo].[s_GetSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetSqlTemplateConfig]
	@sqlTemplateConfigId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[STC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STC_Id] AS Id
	FROM 
		[dbo].[SqlTemplateConfig]
	WHERE 
		[STC_SqlTemplateConfigId] = @sqlTemplateConfigId
END