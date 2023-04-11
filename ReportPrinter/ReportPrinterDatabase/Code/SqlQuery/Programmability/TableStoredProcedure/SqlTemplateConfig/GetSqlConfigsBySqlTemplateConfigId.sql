IF OBJECT_ID('[dbo].[GetSqlConfigsBySqlTemplateConfigId]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetSqlConfigsBySqlTemplateConfigId]
END
GO

CREATE PROCEDURE [dbo].[GetSqlConfigsBySqlTemplateConfigId]
	@sqlTemplateConfigId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[SC_SqlConfigId] AS SqlConfigId,
		[SC_Id] AS Id,
		[SC_DatabaseId] AS DatabaseId,
		[SC_Query] AS Query
	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
	JOIN
		[dbo].[SqlConfig]
	ON
		[STCSC_SqlConfigId] = SC_SqlConfigId
	WHERE 
		[STCSC_SqlTemplateConfigId] = @sqlTemplateConfigId
END