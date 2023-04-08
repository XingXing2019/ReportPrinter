IF OBJECT_ID('[dbo].[GetAllPrintReportSqlVariable]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetAllPrintReportSqlVariable]
END
GO

CREATE PROCEDURE [dbo].[GetAllPrintReportSqlVariable]
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