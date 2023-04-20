IF OBJECT_ID('[dbo].[PdfImageRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfImageRenderer]'), 'IX_PdfImageRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfImageRenderer_PdfRendererBaseId] ON [dbo].[PdfImageRenderer] ([PIR_PdfRendererBaseId])
	END
END