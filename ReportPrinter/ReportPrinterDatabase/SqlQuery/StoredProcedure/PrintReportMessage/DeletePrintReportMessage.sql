USE ReportPrinter;
IF OBJECT_ID('DeletePrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeletePrintReportMessage
END
GO

CREATE PROCEDURE DeletePrintReportMessage
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM PrintReportMessage
	WHERE PRM_MessageId = @messageId
END