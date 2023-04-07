USE ReportPrinter;
IF OBJECT_ID('SqlVariableConfig', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlVariableConfig] (
		[SVC_SqlVariableConfigId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_SqlVariableConfig_SqlVariableConfigId] DEFAULT(NEWID()),
		[SVC_SqlConfigId] UNIQUEIDENTIFIER NOT NULL,
		[SVC_Name] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.SqlVariableConfig] PRIMARY KEY CLUSTERED ([SVC_SqlVariableConfigId]),
		CONSTRAINT [FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId] FOREIGN KEY ([SVC_SqlConfigId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlConfigId]) ON DELETE CASCADE
	);
END

IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlVariableConfig]'), 'IX_SqlVariableConfig_SqlConfigId', 'IndexID') IS NULL
BEGIN
	CREATE INDEX [IX_SqlVariableConfig_SqlConfigId] ON [dbo].[SqlVariableConfig] ([SVC_SqlConfigId])
END