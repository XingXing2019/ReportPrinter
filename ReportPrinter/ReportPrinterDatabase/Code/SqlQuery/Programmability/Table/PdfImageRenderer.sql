IF OBJECT_ID('[dbo].[PdfImageRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfImageRenderer] (
		[PIR_PdfImageRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfImageRenderer_PdfImageRendererId] DEFAULT(NEWID()),
		[PIR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PIR_SourceType] TINYINT NOT NULL,
		[PIR_ImageSource] VARCHAR(MAX) NOT NULL,

		CONSTRAINT [PK_dbo.PdfImageRenderer] PRIMARY KEY CLUSTERED ([PIR_PdfImageRendererId])
	);
END