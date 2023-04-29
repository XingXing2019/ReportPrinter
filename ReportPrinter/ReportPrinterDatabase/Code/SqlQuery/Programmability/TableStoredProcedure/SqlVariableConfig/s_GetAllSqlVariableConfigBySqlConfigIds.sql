IF OBJECT_ID('[dbo].[s_GetAllSqlVariableConfigBySqlConfigIds]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlVariableConfigBySqlConfigIds]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlVariableConfigBySqlConfigIds]
	@sqlConfigIds NVARCHAR(MAX)
AS
BEGIN
	DECLARE @temp TABLE (
		SqlConfigId UNIQUEIDENTIFIER
	);
		
	IF @sqlConfigIds <> ''
	BEGIN
		INSERT INTO @temp
		SELECT value FROM STRING_SPLIT(@sqlConfigIds, ',')
	END

	SELECT 
		[SVC_SqlVariableConfigId] AS SqlVariableConfigId,
		[SVC_SqlConfigId] AS SqlConfigId,
		[SVC_Name] AS Name
	FROM 
		[dbo].[SqlVariableConfig]
	WHERE
		[SVC_SqlConfigId] IN (SELECT SqlConfigId FROM @temp)
END