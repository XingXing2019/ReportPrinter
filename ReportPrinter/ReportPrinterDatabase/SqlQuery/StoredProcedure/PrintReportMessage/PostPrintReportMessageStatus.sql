IF OBJECT_ID('PostPrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PostPrintReportMessage
END
GO

CREATE PROCEDURE PostPrintReportMessage
	@messageId UNIQUEIDENTIFIER,
	@correlationId UNIQUEIDENTIFIER,
	@reportType VARCHAR(10),
	@templateId VARCHAR(100),
	@printerId VARCHAR(100),
	@numberOfCopy INT,
	@hasReprintFlag BIT
AS
BEGIN
	INSERT INTO [dbo].[PrintReportMessage] (
		[PRM_MessageId],
		[PRM_CorrelationId],
		[PRM_ReportType],
		[PRM_TemplateId],
		[PRM_PrinterId],
		[PRM_NumberOfCopy],
		[PRM_HasReprintFlag],
		[PRM_PublishTime],
		[PRM_ReceiveTime],
		[PRM_CompleteTime],
		[PRM_Status]
	) VALUES (
		@messageId,
		@correlationId,
		@reportType,
		@templateId,
		@printerId,
		@numberOfCopy,
		@hasReprintFlag,
		GETDATE(),
		NULL,
		NUll,
		'Publish'
	)
END