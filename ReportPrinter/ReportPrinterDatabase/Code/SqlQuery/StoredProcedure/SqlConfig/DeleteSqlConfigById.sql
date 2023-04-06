IF OBJECT_ID('DeleteSqlConfigById', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteSqlConfigById
END
GO

CREATE PROCEDURE DeleteSqlConfigById
	@sqlConfigId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[SqlConfig]
	WHERE [SC_SqlConfigId] = @sqlConfigId
END