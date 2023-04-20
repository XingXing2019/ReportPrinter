IF OBJECT_ID('[dbo].[PdfPageNumberRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfPageNumberRenderer]'), 'IX_PdfPageNumberRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfPageNumberRenderer_PdfRendererBaseId] ON [dbo].[PdfPageNumberRenderer] ([PPNR_PdfRendererBaseId])
	END
END