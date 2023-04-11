IF OBJECT_ID('[dbo].[SqlTemplateConfig]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlTemplateConfig]'), 'IX_SqlTemplateConfig_Id', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlTemplateConfig_Id] ON [dbo].[SqlTemplateConfig] ([STC_Id])
	END
END