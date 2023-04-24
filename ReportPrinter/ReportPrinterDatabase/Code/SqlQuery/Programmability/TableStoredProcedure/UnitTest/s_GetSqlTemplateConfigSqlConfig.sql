USE ReportPrinterTest
IF OBJECT_ID('[dbo].[s_GetSqlTemplateConfigSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetSqlTemplateConfigSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetSqlTemplateConfigSqlConfig]
	@sqlConfigId UNIQUEIDENTIFIER,
	@sqlTemplateConfigId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[STCSC_SqlTemplateConfigSqlConfigId] AS SqlTemplateConfigSqlConfigId
	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
	WHERE
		[STCSC_SqlConfigId] = @sqlConfigId
	AND
		[STCSC_SqlTemplateConfigId] = @sqlTemplateConfigId
END