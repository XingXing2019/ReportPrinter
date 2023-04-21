IF OBJECT_ID('[dbo].[PdfWaterMarkRenderer]', N'U') IS NOT NULL
BEGIN
	IF OBJECT_ID('[dbo].[FK_PdfWaterMarkRenderer_PdfRendererBase_PdfRendererBaseId]', 'F') IS NULL
	BEGIN		
		ALTER TABLE [dbo].[PdfWaterMarkRenderer]
		ADD CONSTRAINT [FK_PdfWaterMarkRenderer_PdfRendererBase_PdfRendererBaseId] FOREIGN KEY ([PWMR_PdfRendererBaseId])
		REFERENCES [dbo].[PdfRendererBase] ([PRB_PdfRendererBaseId]) ON DELETE CASCADE
	END
END