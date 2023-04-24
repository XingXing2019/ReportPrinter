IF OBJECT_ID('[dbo].[SqlResColumnConfig]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_SqlResColumnConfig_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[SqlResColumnConfig]
		ADD CONSTRAINT [FK_SqlResColumnConfig_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([SRCC_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END