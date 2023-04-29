IF OBJECT_ID('[dbo].[PdfTextRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfTextRenderer]'), 'IX_PdfTextRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfTextRenderer_PdfRendererBaseId] ON [dbo].[PdfTextRenderer] ([PTR_PdfRendererBaseId])
	END
END