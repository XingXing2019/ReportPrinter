IF OBJECT_ID('[dbo].[PdfTextRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfTextRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTextRenderer]
		ADD CONSTRAINT [FK_PdfTextRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PTR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END

	IF OBJECT_ID('[dbo].[FK_PdfTextRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTextRenderer]
		ADD CONSTRAINT [FK_PdfTextRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId] FOREIGN KEY ([PTR_SqlTemplateConfigSqlConfigId])
		REFERENCES [dbo].[SqlTemplateConfigSqlConfig] ([STCSC_SqlTemplateConfigSqlConfigId]) ON DELETE CASCADE
	END
END