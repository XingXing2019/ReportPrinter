IF OBJECT_ID('[dbo].[s_GetAllSqlConfigBySqlTemplateConfigIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlConfigBySqlTemplateConfigIds]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlConfigBySqlTemplateConfigIds]
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
		[SC_SqlConfigId] AS SqlConfigId,
		[SC_Id] AS Id,
		[SC_DatabaseId] AS DatabaseId,
		[SC_Query] AS Query
	FROM 
		[dbo].[SqlConfig]
	JOIN
		[dbo].[SqlTemplateConfigSqlConfig]
	ON
		SC_SqlConfigId = STCSC_SqlConfigId
	WHERE
		STCSC_SqlTemplateConfigId IN (SELECT SqlTempalteConfigId FROM @temp)
END