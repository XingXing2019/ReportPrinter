IF OBJECT_ID('[dbo].[PdfBarcodeRenderer]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfBarcodeRenderer]'), 'IX_PdfBarcodeRenderer_PdfRendererBaseId', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfBarcodeRenderer_PdfRendererBaseId] ON [dbo].[PdfBarcodeRenderer] ([PBR_PdfRendererBaseId])
	END
END