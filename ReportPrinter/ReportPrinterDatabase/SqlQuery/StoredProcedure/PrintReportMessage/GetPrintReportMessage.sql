IF OBJECT_ID('GetPrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE GetPrintReportMessage
END
GO

CREATE PROCEDURE GetPrintReportMessage
	@messageId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		[PRM_MessageId] AS MessageId,
		[PRM_CorrelationId] AS CorrelationId,
		[PRM_ReportType] AS ReportType,
		[PRM_TemplateId] AS TemplateId,
		[PRM_PrinterId] AS PrinterId,
		[PRM_NumberOfCopy] AS NumberOfCopy,
		[PRM_HasReprintFlag] AS HasReprintFlag,
		[PRM_PublishTime] AS PublishTime,
		[PRM_ReceiveTime] AS ReceiveTime,
		[PRM_CompleteTime] AS CompleteTime,
		[PRM_Status] AS Status,
		[PRSV_Name] AS Name,
		[PRSV_Value] AS Value
	FROM 
		[dbo].[PrintReportMessage]
	LEFT JOIN 
		[dbo].PrintReportSqlVariable
	ON 
		[PRM_MessageId] = [PRSV_MessageId]
	WHERE 
		[PRM_MessageId] = @messageId
END