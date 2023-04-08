IF OBJECT_ID('[dbo].[SqlTemplateConfig]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlTemplateConfig] (
		[STC_SqlTemplateConfigId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_SqlTemplateConfig_SqlTemplateConfigId] DEFAULT(NEWID()),
		[STC_Id] VARCHAR(100) NOT NULL,

		CONSTRAINT [PK_dbo.SqlTemplateConfig] PRIMARY KEY CLUSTERED ([STC_SqlTemplateConfigId])
	);
END