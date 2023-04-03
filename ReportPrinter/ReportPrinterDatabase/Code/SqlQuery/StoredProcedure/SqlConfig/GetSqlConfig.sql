IF OBJECT_ID('GetSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE GetSqlConfig
END
GO

CREATE PROCEDURE GetSqlConfig
	@sqlConfigId UNIQUEIDENTIFIER
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
		[SC_SqlConfigId] = @sqlConfigId
END