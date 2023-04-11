IF OBJECT_ID('[dbo].[SqlVariableConfig]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlVariableConfig]'), 'IX_SqlVariableConfig_SqlConfigId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlVariableConfig_SqlConfigId] ON [dbo].[SqlVariableConfig] ([SVC_SqlConfigId])
	END
END