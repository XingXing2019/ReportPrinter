IF OBJECT_ID('[dbo].[PrintReportMessage]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PrintReportMessage] (
		[PRM_MessageId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PrintReportMessage_MessageId] DEFAULT(NEWID()),
		[PRM_CorrelationId] UNIQUEIDENTIFIER NULL,
		[PRM_ReportType] VARCHAR(10) NOT NULL CHECK([PRM_ReportType] IN ('PDF', 'Label')),
		[PRM_TemplateId] VARCHAR(100) NOT NULL,
		[PRM_PrinterId] VARCHAR(100) NULL,
		[PRM_NumberOfCopy] INT NOT NULL,
		[PRM_HasReprintFlag] BIT NULL,
		[PRM_PublishTime] DATETIME NULL,
		[PRM_ReceiveTime] DATETIME NULL,
		[PRM_CompleteTime] DATETIME NULL,
		[PRM_Status] VARCHAR(20) NOT NULL CHECK([PRM_Status] IN ('Publish', 'Receive', 'Complete', 'Error')),

		CONSTRAINT [PK_dbo.PrintReportMessage] PRIMARY KEY CLUSTERED ([PRM_MessageId])
	);
END