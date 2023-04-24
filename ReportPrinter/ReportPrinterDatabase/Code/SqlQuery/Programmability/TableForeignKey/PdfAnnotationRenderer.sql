IF OBJECT_ID('[dbo].[PdfAnnotationRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfAnnotationRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfAnnotationRenderer]
		ADD CONSTRAINT [FK_PdfAnnotationRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PAR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END

	IF OBJECT_ID('[dbo].[FK_PdfAnnotationRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfAnnotationRenderer]
		ADD CONSTRAINT [FK_PdfAnnotationRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId] FOREIGN KEY ([PAR_SqlTemplateConfigSqlConfigId])
		REFERENCES [dbo].[SqlTemplateConfigSqlConfig] ([STCSC_SqlTemplateConfigSqlConfigId]) ON DELETE CASCADE
	END
END