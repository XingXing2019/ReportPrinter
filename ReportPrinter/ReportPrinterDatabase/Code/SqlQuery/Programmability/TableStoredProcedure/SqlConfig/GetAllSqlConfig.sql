IF OBJECT_ID('[dbo].[GetAllSqlConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllSqlConfig]
END
GO

CREATE PROCEDURE [dbo].[GetAllSqlConfig]
AS
BEGIN
	SELECT 
		[SC_SqlConfigId] AS SqlConfigId,
		[SC_Id] AS Id,
		[SC_DatabaseId] AS DatabaseId,
		[SC_Query] AS Query
	FROM 
		[dbo].[SqlConfig]
END