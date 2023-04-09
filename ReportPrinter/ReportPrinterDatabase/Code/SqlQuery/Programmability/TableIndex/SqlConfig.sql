IF OBJECT_ID('[dbo].[SqlConfig]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlConfig]'), 'IX_SqlConfig_DatabaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlConfig_DatabaseId] ON [dbo].[SqlConfig] ([SC_DatabaseId])
	END
END