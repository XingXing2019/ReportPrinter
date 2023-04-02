USE ReportPrinter;
IF OBJECT_ID('SqlConfig', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[SqlConfig] (
		[SC_SqlId] UNIQUEIDENTIFIER NOT NULL,
		[SC_Id] VARCHAR(100) NOT NULL,
		[SC_DatabaseId] VARCHAR(100) NOT NULL,
		[SC_Query] NVARCHAR(MAX) NOT NULL,

		CONSTRAINT [PK_dbo.SqlConfig] PRIMARY KEY CLUSTERED ([SC_SqlId])
	);
END