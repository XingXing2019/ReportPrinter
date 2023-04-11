IF OBJECT_ID('[dbo].[GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]
END
GO

CREATE PROCEDURE [dbo].[GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds]
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