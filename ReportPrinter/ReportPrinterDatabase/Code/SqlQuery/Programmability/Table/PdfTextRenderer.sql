IF OBJECT_ID('[dbo].[PdfTextRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfTextRenderer] (
		[PTR_PdfTextRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfTextRenderer_PdfTextRendererId] DEFAULT(NEWID()),
		[PTR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PTR_TextRendererType] TINYINT NOT NULL,
		[PTR_Content] VARCHAR(200) NULL, 
		[PTR_SqlTemplateConfigSqlConfigId] UNIQUEIDENTIFIER NULL,
		[PTR_Mask] VARCHAR(50) NULL,
		[PTR_Title] VARCHAR(50) NULL,

		CONSTRAINT [PK_dbo.PdfTextRenderer] PRIMARY KEY CLUSTERED ([PTR_PdfTextRendererId])
	);
END