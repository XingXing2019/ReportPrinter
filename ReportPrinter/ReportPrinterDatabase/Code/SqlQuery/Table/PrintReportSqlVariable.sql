IF OBJECT_ID('PrintReportSqlVariable', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PrintReportSqlVariable] (
		[PRSV_SqlVariableId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PrintReportSqlVariable_SqlVariableId] DEFAULT(NEWID()),
		[PRSV_MessageId] UNIQUEIDENTIFIER NOT NULL,
		[PRSV_Name] VARCHAR(100) NOT NULL,
		[PRSV_Value] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.PrintReportSqlVariable] PRIMARY KEY CLUSTERED ([PRSV_SqlVariableId]),
		CONSTRAINT [FK_dbo.PrintReportMessage_dbo.PrintReportSqlVariable_MessageId] FOREIGN KEY ([PRSV_MessageId])
		REFERENCES [dbo].[PrintReportMessage] ([PRM_MessageId]) ON DELETE CASCADE
	);
END

IF INDEXPROPERTY(OBJECT_ID('[dbo].[PrintReportSqlVariable]'), 'IX_PrintReportSqlVariable_MessageId', 'IndexID') IS NULL
BEGIN
	CREATE INDEX [IX_PrintReportSqlVariable_MessageId] ON [dbo].[PrintReportSqlVariable] ([PRSV_MessageId])
END