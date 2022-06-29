IF OBJECT_ID('DeleteAllPrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteAllPrintReportMessage
END
GO

CREATE PROCEDURE DeleteAllPrintReportMessage
AS
BEGIN
	DELETE FROM [dbo].[PrintReportMessage]
END