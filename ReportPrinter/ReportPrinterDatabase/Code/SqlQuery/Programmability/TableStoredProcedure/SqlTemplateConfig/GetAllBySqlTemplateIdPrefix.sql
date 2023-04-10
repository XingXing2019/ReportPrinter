IF OBJECT_ID('[dbo].[GetAllBySqlTemplateIdPrefix]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllBySqlTemplateIdPrefix]
END
GO

CREATE PROCEDURE [dbo].[GetAllBySqlTemplateIdPrefix]
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