IF OBJECT_ID('[dbo].[PdfReprintMarkRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfReprintMarkRenderer]'), 'IX_PdfReprintMarkRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfReprintMarkRenderer_PdfRendererBaseId] ON [dbo].[PdfReprintMarkRenderer] ([PRMR_PdfRendererBaseId])
	END
END