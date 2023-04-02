USE ReportPrinter;
IF OBJECT_ID('SqlVariableConfig', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlVariableConfig] (
		[SVC_SqlVariableId] UNIQUEIDENTIFIER NOT NULL,
		[SVC_SqlId] UNIQUEIDENTIFIER NOT NULL,
		[SVC_Name] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.SqlVariableConfig] PRIMARY KEY CLUSTERED ([SVC_SqlVariableId]),
		CONSTRAINT [FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId] FOREIGN KEY ([SVC_SqlId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlId]) ON DELETE CASCADE
	);
END