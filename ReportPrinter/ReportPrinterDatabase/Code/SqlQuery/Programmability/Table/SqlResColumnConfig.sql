IF OBJECT_ID('[dbo].[SqlResColumnConfig]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlResColumnConfig] (
		[SRCC_SqlResColumnConfigId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_SqlResColumnConfig_SqlResColumnConfigId] DEFAULT(NEWID()),
		[SRCC_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL,
		[SRCC_Id] VARCHAR(50) NOT NULL,
		[SRCC_Title] VARCHAR(50) NULL,
		[SRCC_WidthRatio] FLOAT NULL,
		[SRCC_Position] TINYINT NULL,
		[SRCC_Left] FLOAT NULL,
		[SRCC_Right] FLOAT NULL,

		CONSTRAINT [PK_dbo.SqlResColumnConfig] PRIMARY KEY CLUSTERED ([SRCC_SqlResColumnConfigId])
	);
END