IF OBJECT_ID('[dbo].[SqlVariableConfig]', N'U') IS NOT NULL
BEGIN	
	IF OBJECT_ID('[dbo].[FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[SqlVariableConfig]
		ADD CONSTRAINT [FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId] FOREIGN KEY ([SVC_SqlConfigId])
		REFERENCES [dbo].[SqlConfig] ([SC_SqlConfigId]) ON DELETE CASCADE
	END
END