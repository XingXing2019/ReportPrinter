IF OBJECT_ID('[dbo].[PdfBarcodeRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfBarcodeRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfBarcodeRenderer]
		ADD CONSTRAINT [FK_PdfBarcodeRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PBR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END