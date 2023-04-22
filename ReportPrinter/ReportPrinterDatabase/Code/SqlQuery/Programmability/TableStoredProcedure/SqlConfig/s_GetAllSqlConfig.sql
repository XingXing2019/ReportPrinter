IF OBJECT_ID('[dbo].[s_GetAllSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllSqlConfig]
AS
BEGIN
	SELECT 
		[SC_SqlConfigId] AS SqlConfigId,
		[SC_Id] AS Id,
		[SC_DatabaseId] AS DatabaseId,
		[SC_Query] AS Query
	FROM 
		[dbo].[SqlConfig]
	ORDER BY
		[SC_DatabaseId]
END