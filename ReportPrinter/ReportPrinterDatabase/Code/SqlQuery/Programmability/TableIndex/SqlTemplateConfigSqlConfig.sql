IF OBJECT_ID('[dbo].[SqlTemplateConfigSqlConfig]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlTemplateConfigSqlConfig]'), 'IX_SqlTemplateConfigSqlConfig_SqlTemplateConfigId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlTemplateConfigSqlConfig_SqlTemplateConfigId] ON [dbo].[SqlTemplateConfigSqlConfig] ([STCSC_SqlTemplateConfigId])
	END

	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlTemplateConfigSqlConfig]'), 'IX_SqlTemplateConfigSqlConfig_SqlConfigId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlTemplateConfigSqlConfig_SqlConfigId] ON [dbo].[SqlTemplateConfigSqlConfig] ([STCSC_SqlConfigId])
	END
END