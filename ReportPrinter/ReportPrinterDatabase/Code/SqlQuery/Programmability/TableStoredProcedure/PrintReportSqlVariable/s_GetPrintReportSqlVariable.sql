IF OBJECT_ID('[dbo].[s_GetPrintReportSqlVariable]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPrintReportSqlVariable]
END
GO

CREATE PROCEDURE [dbo].[s_GetPrintReportSqlVariable]
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