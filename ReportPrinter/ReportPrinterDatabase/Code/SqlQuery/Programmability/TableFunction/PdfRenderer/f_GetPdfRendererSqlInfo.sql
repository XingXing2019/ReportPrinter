IF OBJECT_ID('[dbo].[f_GetPdfRendererSqlInfo]', 'TF') IS NOT NULL
BEGIN
	DROP FUNCTION [dbo].[f_GetPdfRendererSqlInfo]
END
GO

CREATE FUNCTION [dbo].[f_GetPdfRendererSqlInfo] (
	@sqlTemplateConfigSqlConfigId UNIQUEIDENTIFIER
)
RETURNS @Result TABLE (
	[SqlTemplateId] VARCHAR(50) NOT NULL,
	[SqlId] VARCHAR(50) NOT NULL
)
AS
BEGIN
	INSERT INTO
		@Result
	SELECT 
		[STC_Id] AS SqlTemplateId,
		[SC_Id] AS SqlId
 	FROM 
		[dbo].[SqlTemplateConfigSqlConfig]
	JOIN 
		[dbo].[SqlTemplateConfig] ON [STC_SqlTemplateConfigId] = [STCSC_SqlTemplateConfigId]
	JOIN
		[dbo].[SqlConfig] ON [SC_SqlConfigId] = [STCSC_SqlConfigId]
	WHERE 
		[STCSC_SqlTemplateConfigSqlConfigId] = @sqlTemplateConfigSqlConfigId

	RETURN
END