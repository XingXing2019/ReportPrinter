IF OBJECT_ID('[dbo].[GetPrintReportSqlVariable]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPrintReportSqlVariable]
END
GO

CREATE PROCEDURE [dbo].[GetPrintReportSqlVariable]
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[PRSV_SqlVariableId] AS SqlVariableId,
		[PRSV_MessageId] AS MessageId,
		[PRSV_Name] AS Name,
		[PRSV_Value] AS Value
	FROM 
		[dbo].[PrintReportSqlVariable]
	WHERE
		[PRSV_MessageId] = @messageId
END