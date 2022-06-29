USE ReportPrinter;
IF OBJECT_ID('DeleteAllPrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeleteAllPrintReportMessage
END
GO

CREATE PROCEDURE DeleteAllPrintReportMessage
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM PrintReportMessage
	WHERE PRM_MessageId = @messageId
END