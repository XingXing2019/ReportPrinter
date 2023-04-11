IF OBJECT_ID('[dbo].[GetSqlTemplateConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetSqlTemplateConfig]
END
GO

CREATE PROCEDURE [dbo].[GetSqlTemplateConfig]
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