IF OBJECT_ID('[dbo].[s_GetAllPrintReportSqlVariable]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetAllPrintReportSqlVariable]
END
GO

CREATE PROCEDURE [dbo].[s_GetAllPrintReportSqlVariable]
AS
BEGIN
	SELECT 
		[PRSV_SqlVariableId] AS SqlVariableId,
		[PRSV_MessageId] AS MessageId,
		[PRSV_Name] AS Name,
		[PRSV_Value] AS Value
	FROM 
		[dbo].[PrintReportSqlVariable]
END