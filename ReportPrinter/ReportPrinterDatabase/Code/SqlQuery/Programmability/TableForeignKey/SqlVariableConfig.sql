IF OBJECT_ID('[dbo].[SqlVariableConfig]', N'U') IS NOT NULL
BEGIN	
	IF OBJECT_ID('[dbo].[FK_SqlVariableConfig_SqlConfig_SqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[SqlVariableConfig]
		ADD CONSTRAINT [FK_SqlVariableConfig_SqlConfig_SqlConfigId] FOREIGN KEY ([SVC_SqlConfigId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlConfigId]) ON DELETE CASCADE
	END
END