IF OBJECT_ID('[dbo].[s_GetSqlConfigsBySqlTemplateConfigId]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetSqlConfigsBySqlTemplateConfigId]
END
GO

CREATE PROCEDURE [dbo].[s_GetSqlConfigsBySqlTemplateConfigId]
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