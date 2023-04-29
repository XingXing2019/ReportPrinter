IF OBJECT_ID('[dbo].[PdfReprintMarkRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfReprintMarkRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfReprintMarkRenderer]
		ADD CONSTRAINT [FK_PdfReprintMarkRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PRMR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END