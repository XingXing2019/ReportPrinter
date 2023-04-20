IF OBJECT_ID('[dbo].[PdfRendererBase]', N'U') IS NOT NULL
BEGIN
	IF INDEXPROPERTY(OBJECT_ID('[dbo].[PdfRendererBase]'), 'IX_PdfRendererBase_Id', 'IndexID') IS NULL
	BEGIN
		CREATE INDEX [IX_PdfRendererBase_Id] ON [dbo].[PdfRendererBase] ([PRB_ID])
	END
END