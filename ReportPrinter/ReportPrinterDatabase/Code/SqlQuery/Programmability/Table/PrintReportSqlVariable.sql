IF OBJECT_ID('[dbo].[PrintReportSqlVariable]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PrintReportSqlVariable] (
		[PRSV_SqlVariableId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PrintReportSqlVariable_SqlVariableId] DEFAULT(NEWID()),
		[PRSV_MessageId] UNIQUEIDENTIFIER NOT NULL,
		[PRSV_Name] VARCHAR(100) NOT NULL,
		[PRSV_Value] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.PrintReportSqlVariable] PRIMARY KEY CLUSTERED ([PRSV_SqlVariableId])
	);
END