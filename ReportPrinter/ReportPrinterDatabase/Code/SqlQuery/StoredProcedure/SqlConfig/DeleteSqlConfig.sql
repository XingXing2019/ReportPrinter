IF OBJECT_ID('DeleteSqlConfig', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteSqlConfig
END
GO

CREATE PROCEDURE DeleteSqlConfig
	@sqlConfigId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[SqlConfig]
	WHERE [SC_SqlConfigId] = @sqlConfigId
END