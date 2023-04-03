USE ReportPrinter;
IF OBJECT_ID('SqlVariableConfig', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlVariableConfig] (
		[SVC_SqlVariableConfigId] UNIQUEIDENTIFIER NOT NULL,
		[SVC_SqlConfigId] UNIQUEIDENTIFIER NOT NULL,
		[SVC_Name] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.SqlVariableConfig] PRIMARY KEY CLUSTERED ([SVC_SqlVariableConfigId]),
		CONSTRAINT [FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId] FOREIGN KEY ([SVC_SqlConfigId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlConfigId]) ON DELETE CASCADE
	);
END