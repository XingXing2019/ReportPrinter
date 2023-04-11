IF OBJECT_ID('[dbo].[PrintReportSqlVariable]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PrintReportSqlVariable]'), 'IX_PrintReportSqlVariable_MessageId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PrintReportSqlVariable_MessageId] ON [dbo].[PrintReportSqlVariable] ([PRSV_MessageId])
	END
END