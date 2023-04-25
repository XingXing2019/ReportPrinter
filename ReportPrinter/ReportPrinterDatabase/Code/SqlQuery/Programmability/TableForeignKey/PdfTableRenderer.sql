IF OBJECT_ID('[dbo].[PdfTableRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfTableRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTableRenderer]
		ADD CONSTRAINT [FK_PdfTableRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PTR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END

	IF OBJECT_ID('[dbo].[FK_PdfTableRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTableRenderer]
		ADD CONSTRAINT [FK_PdfTableRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId] FOREIGN KEY ([PTR_SqlTemplateConfigSqlConfigId])
		REFERENCES [dbo].[SqlTemplateConfigSqlConfig] ([STCSC_SqlTemplateConfigSqlConfigId]) ON DELETE CASCADE
	END

	IF OBJECT_ID('[dbo].[FK_PdfTableRenderer_PdfTableRenderer_PdfTableRendererId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTableRenderer]
		ADD CONSTRAINT [FK_PdfTableRenderer_PdfTableRenderer_PdfTableRendererId] FOREIGN KEY ([PTR_SubPdfTableRendererId])
		REFERENCES [dbo].[PdfTableRenderer] ([PTR_PdfTableRendererId]) ON DELETE NO ACTION
	END
END