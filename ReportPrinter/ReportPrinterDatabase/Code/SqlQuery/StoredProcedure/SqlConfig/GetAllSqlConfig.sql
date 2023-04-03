IF OBJECT_ID('GetAllSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE GetAllSqlConfig
END
GO

CREATE PROCEDURE GetAllSqlConfig
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