IF OBJECT_ID('[dbo].[PrintReportSqlVariable]', N'U') IS NOT NULL
BEGIN	
	IF OBJECT_ID('[dbo].[FK_dbo.PrintReportMessage_dbo.PrintReportSqlVariable_MessageId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PrintReportSqlVariable]
		ADD CONSTRAINT [FK_dbo.PrintReportMessage_dbo.PrintReportSqlVariable_MessageId] FOREIGN KEY ([PRSV_MessageId])
		REFERENCES [dbo].[PrintReportMessage] ([PRM_MessageId]) ON DELETE CASCADE
	END
END