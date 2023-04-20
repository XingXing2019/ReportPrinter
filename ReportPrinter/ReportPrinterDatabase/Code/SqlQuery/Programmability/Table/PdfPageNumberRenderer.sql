IF OBJECT_ID('[dbo].[PdfPageNumberRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfPageNumberRenderer] (
		[PPNR_PdfPageNumberRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfPageNumberRenderer_PdfPageNumberRendererId] DEFAULT(NEWID()),
		[PPNR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PPNR_StartPage] INT NULL,
		[PPNR_EndPage] INT NULL,
		[PPNR_PageNumberLocation] TINYINT NULL,

		CONSTRAINT [PK_dbo.PdfPageNumberRenderer] PRIMARY KEY CLUSTERED ([PPNR_PdfPageNumberRendererId])
	);
END