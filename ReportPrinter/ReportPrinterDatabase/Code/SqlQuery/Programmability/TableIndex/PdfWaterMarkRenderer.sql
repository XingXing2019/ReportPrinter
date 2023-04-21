IF OBJECT_ID('[dbo].[PdfWaterMarkRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfWaterMarkRenderer]'), 'IX_PdfWaterMarkRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfWaterMarkRenderer_PdfRendererBaseId] ON [dbo].[PdfWaterMarkRenderer] ([PWMR_PdfRendererBaseId])
	END
END