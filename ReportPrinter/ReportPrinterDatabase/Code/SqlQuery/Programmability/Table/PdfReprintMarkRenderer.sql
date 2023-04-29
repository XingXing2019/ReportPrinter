IF OBJECT_ID('[dbo].[PdfReprintMarkRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfReprintMarkRenderer] (
		[PRMR_PdfReprintMarkRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfReprintMarkRenderer_PdfReprintMarkRendererId] DEFAULT(NEWID()),
		[PRMR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PRMR_Text] VARCHAR(100) NOT NULL,
		[PRMR_BoardThickness] FLOAT NULL,
		[PRMR_Location] TINYINT NULL,

		CONSTRAINT [PK_dbo.PdfReprintMarkRenderer] PRIMARY KEY CLUSTERED ([PRMR_PdfReprintMarkRendererId])
	);
END