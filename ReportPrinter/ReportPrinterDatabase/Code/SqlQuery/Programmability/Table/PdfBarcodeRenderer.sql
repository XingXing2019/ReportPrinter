IF OBJECT_ID('[dbo].[PdfBarcodeRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfBarcodeRenderer] (
		[PBR_PdfBarcodeRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfBarcodeRenderer_PdfBarcodeRendererId] DEFAULT(NEWID()),
		[PBR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PBR_BarcodeFormat] TINYINT NULL,
		[PBR_ShowBarcodeText] BIT NOT NULL CONSTRAINT [DF_PdfBarcodeRenderer_ShowBarcodeText] DEFAULT(0),
		[PBR_SqlTemplateId] VARCHAR(50) NULL,
		[PBR_SqlId] VARCHAR(50) NULL,
		[PBR_SqlResColumn] VARCHAR(50) NULL,

		CONSTRAINT [PK_dbo.PdfBarcodeRenderer] PRIMARY KEY CLUSTERED ([PBR_PdfBarcodeRendererId])
	);
END