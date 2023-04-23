IF OBJECT_ID('[dbo].[SqlTemplateConfigSqlConfig]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlTemplateConfigSqlConfig] (
		[STCSC_SqlTemplateConfigSqlConfigId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId] DEFAULT(NEWID()),
		[STCSC_SqlTemplateConfigId] UNIQUEIDENTIFIER NOT NULL,
		[STCSC_SqlConfigId] UNIQUEIDENTIFIER NOT NULL,

		CONSTRAINT [PK_dbo.SqlTemplateConfigSqlConfig] PRIMARY KEY CLUSTERED ([STCSC_SqlTemplateConfigSqlConfigId])
	);
END