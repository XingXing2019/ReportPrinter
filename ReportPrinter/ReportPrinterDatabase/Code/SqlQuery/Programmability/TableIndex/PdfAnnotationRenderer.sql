IF OBJECT_ID('[dbo].[PdfAnnotationRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfAnnotationRenderer]'), 'IX_PdfAnnotationRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfAnnotationRenderer_PdfRendererBaseId] ON [dbo].[PdfAnnotationRenderer] ([PAR_PdfRendererBaseId])
	END
END