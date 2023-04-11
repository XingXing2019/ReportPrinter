IF OBJECT_ID('[dbo].[SqlTemplateConfigSqlConfig]', N'U') IS NOT NULL
BEGIN
	
	IF OBJECT_ID('[dbo].[FK_SqlTemplateConfigSqlConfig_SqlConfig_SqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[SqlTemplateConfigSqlConfig]
		ADD CONSTRAINT [FK_SqlTemplateConfigSqlConfig_SqlTemplateConfig_SqlTemplateConfigId] FOREIGN KEY ([STCSC_SqlTemplateConfigId])
		REFERENCES [dbo].[SqlTemplateConfig] ([STC_SqlTemplateConfigId]) ON DELETE CASCADE
	END

	IF OBJECT_ID('[dbo].[FK_SqlTemplateConfigSqlConfig_SqlConfig_SqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[SqlTemplateConfigSqlConfig]
		ADD CONSTRAINT [FK_SqlTemplateConfigSqlConfig_SqlConfig_SqlConfigId] FOREIGN KEY ([STCSC_SqlConfigId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlConfigId]) ON DELETE CASCADE
	END
END

