IF OBJECT_ID('[dbo].[SqlResColumnConfig]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[SqlResColumnConfig]'), 'IX_SqlResColumnConfig_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_SqlResColumnConfig_PdfRendererBaseId] ON [dbo].[SqlResColumnConfig] ([SRCC_PdfRendererBaseId])
	END
END