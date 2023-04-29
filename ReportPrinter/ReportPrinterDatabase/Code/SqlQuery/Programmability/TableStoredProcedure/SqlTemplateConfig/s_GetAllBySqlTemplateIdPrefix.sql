IF OBJECT_ID('[dbo].[s_GetAllBySqlTemplateIdPrefix]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllBySqlTemplateIdPrefix]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllBySqlTemplateIdPrefix]
	@sqlTemplateIdPrefix VARCHAR(50)
AS
BEGIN
	SELECT 
		[STC_SqlTemplateConfigId] AS SqlTemplateConfigId,
		[STC_Id] AS Id
	FROM 
		[dbo].[SqlTemplateConfig]
	WHERE
		[STC_Id] LIKE @sqlTemplateIdPrefix + '%'
	ORDER BY
		[STC_Id]
END