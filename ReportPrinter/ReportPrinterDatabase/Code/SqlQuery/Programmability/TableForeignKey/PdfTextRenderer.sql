IF OBJECT_ID('[dbo].[PdfTextRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfTextRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfTextRenderer]
		ADD CONSTRAINT [FK_PdfTextRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PTR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END