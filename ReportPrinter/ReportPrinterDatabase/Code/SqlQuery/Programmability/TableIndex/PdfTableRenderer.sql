IF OBJECT_ID('[dbo].[PdfTableRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfTableRenderer]'), 'IX_PdfTableRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX IX_PdfTableRenderer_PdfRendererBaseId ON [dbo].[PdfTableRenderer] ([PTR_PdfRendererBaseId])
	END
END