IF OBJECT_ID('[dbo].[s_GetAllByDatabaseIdPrefix]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllByDatabaseIdPrefix]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllByDatabaseIdPrefix]
	@databaseIdPrefix VARCHAR(20)
AS
BEGIN
	SELECT 
		[SC_SqlConfigId] AS SqlConfigId,
		[SC_Id] AS Id,
		[SC_DatabaseId] AS DatabaseId,
		[SC_Query] AS Query
	FROM 
		[dbo].[SqlConfig]
	WHERE
		[SC_DatabaseId] LIKE @databaseIdPrefix + '%'
	ORDER BY
		[SC_DatabaseId]
END