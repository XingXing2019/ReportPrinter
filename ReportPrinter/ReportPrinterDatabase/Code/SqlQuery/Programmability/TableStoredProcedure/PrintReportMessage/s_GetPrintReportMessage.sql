IF OBJECT_ID('[dbo].[s_GetPrintReportMessage]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_GetPrintReportMessage]
END
GO

CREATE PROCEDURE [dbo].[s_GetPrintReportMessage]
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
		[PRM_Status] AS Status
	FROM 
		[dbo].[PrintReportMessage]
	WHERE 
		[PRM_MessageId] = @messageId
END