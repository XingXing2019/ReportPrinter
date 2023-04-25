IF OBJECT_ID('[dbo].[PdfTableRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfTableRenderer] (
		[PTR_PdfTableRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfTableRenderer_PdfTableRendererId] DEFAULT(NEWID()),
		[PTR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PTR_BoardThickness] FLOAT NULL,
		[PTR_LineSpace] FLOAT NULL,
		[PTR_TitleHorizontalAlignment] TINYINT NULL,
		[PTR_HideTitle] BIT NULL,
		[PTR_Space] FLOAT NULL,
		[PTR_TitleColor] TINYINT NULL,
		[PTR_TitleColorOpacity] FLOAT NULL,
		[PTR_SqlTemplateConfigSqlConfigId] UNIQUEIDENTIFIER NOT NULL,
		[PTR_SqlVariable] VARCHAR(50) NULL,
		[PTR_SubPdfTableRendererId] UNIQUEIDENTIFIER NULL,		

		CONSTRAINT [PK_dbo.PdfTableRenderer] PRIMARY KEY CLUSTERED ([PTR_PdfTableRendererId])
	);
END