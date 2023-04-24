IF OBJECT_ID('[dbo].[PdfAnnotationRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfAnnotationRenderer] (
		[PAR_PdfAnnotationRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfAnnotationRenderer_PdfAnnotationRendererId] DEFAULT(NEWID()),
		[PAR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PAR_AnnotationRendererType] TINYINT NOT NULL,
		[PAR_Title] VARCHAR(MAX) NULL,
		[PAR_Icon] TINYINT NULL,
		[PAR_Content] VARCHAR(MAX) NULL,
		[PAR_SqlTemplateConfigSqlConfigId] UNIQUEIDENTIFIER NULL,

		CONSTRAINT [PK_dbo.PdfAnnotationRenderer] PRIMARY KEY CLUSTERED ([PAR_PdfAnnotationRendererId])
	);
END