IF OBJECT_ID('PostPrintReportSqlVariable', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PostPrintReportSqlVariable
END
GO

CREATE PROCEDURE PostPrintReportSqlVariable
	@messageId UNIQUEIDENTIFIER,
	@name VARCHAR(100),
	@value VARCHAR(100)
AS
BEGIN
	INSERT INTO [dbo].[PrintReportSqlVariable] (
		[PRSV_SqlVariableId],
		[PRSV_MessageId],
		[PRSV_Name],
		[PRSV_Value]
	) VALUES (
		NEWID(),
		@messageId,
		@name,
		@value
	)
END