IF OBJECT_ID('DeletePrintReportMessageById', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeletePrintReportMessageById
END
GO

CREATE PROCEDURE DeletePrintReportMessageById
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	DELETE FROM [dbo].[PrintReportMessage]
	WHERE [PRM_MessageId] = @messageId
END