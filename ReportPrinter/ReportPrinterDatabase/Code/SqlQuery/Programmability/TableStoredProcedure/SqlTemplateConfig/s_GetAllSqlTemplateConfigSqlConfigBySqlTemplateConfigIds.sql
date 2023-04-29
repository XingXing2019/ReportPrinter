IF OBJECT_ID('[dbo].[s_GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]
	@sqlTemplateConfigIds NVARCHAR(MAX)
AS
BEGIN
	DECLARE @temp TABLE (
		SqlTempalteConfigId UNIQUEIDENTIFIER
	);
		
	IF @sqlTemplateConfigIds <> ''
	BEGIN
		INSERT INTO @temp
		SELECT value FROM STRING_SPLIT(@sqlTemplateConfigIds, ',')
	END

	SELECT 
		[STCSC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STCSC_SqlConfigId] AS SqlConfigId
	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
	WHERE
		[STCSC_SqlTemplateConfigId] IN (SELECT SqlTempalteConfigId FROM @temp)
END