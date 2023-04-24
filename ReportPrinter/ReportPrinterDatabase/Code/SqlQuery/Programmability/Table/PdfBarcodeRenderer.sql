IF OBJECT_ID('[dbo].[PdfBarcodeRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfBarcodeRenderer] (
		[PBR_PdfBarcodeRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfBarcodeRenderer_PdfBarcodeRendererId] DEFAULT(NEWID()),
		[PBR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PBR_BarcodeFormat] INT NULL,
		[PBR_ShowBarcodeText] BIT NOT NULL CONSTRAINT [DF_PdfBarcodeRenderer_ShowBarcodeText] DEFAULT(0),
		[PBR_SqlTemplateConfigSqlConfigId] UNIQUEIDENTIFIER NOT NULL,

		CONSTRAINT [PK_dbo.PdfBarcodeRenderer] PRIMARY KEY CLUSTERED ([PBR_PdfBarcodeRendererId])
	);
END