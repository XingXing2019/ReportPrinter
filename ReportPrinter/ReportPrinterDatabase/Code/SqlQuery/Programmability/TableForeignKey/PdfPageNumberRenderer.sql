IF OBJECT_ID('[dbo].[PdfPageNumberRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfPageNumberRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfPageNumberRenderer]
		ADD CONSTRAINT [FK_PdfPageNumberRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PPNR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END