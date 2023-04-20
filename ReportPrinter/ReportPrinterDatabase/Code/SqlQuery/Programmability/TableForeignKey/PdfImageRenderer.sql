IF OBJECT_ID('[dbo].[PdfImageRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfImageRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfImageRenderer]
		ADD CONSTRAINT [FK_PdfImageRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PIR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END