IF OBJECT_ID('[dbo].[PdfWaterMarkRenderer]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfWaterMarkRenderer] (
		[PWMR_PdfWaterMarkRendererId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfWaterMarkRenderer_PdfWaterMarkRendererId] DEFAULT(NEWID()),
		[PWMR_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[PWMR_WaterMarkType] TINYINT NOT NULL,
		[PWMR_Content] VARCHAR(200) NULL,
		[PWMR_Location] TINYINT NULL,
		[PWMR_SqlTemplateId] VARCHAR(50) NULL,
		[PWMR_SqlId] VARCHAR(50) NULL,
		[PWMR_SqlResColumn] VARCHAR(50) NULL,
		[PWMR_StartPage] INT NULL,
		[PWMR_EndPage] INT NULL,
		[PWMR_Rotate] FLOAT NULL,

		CONSTRAINT [PK_dbo.PdfWaterMarkRenderer] PRIMARY KEY CLUSTERED ([PWMR_PdfWaterMarkRendererId])
	);
END