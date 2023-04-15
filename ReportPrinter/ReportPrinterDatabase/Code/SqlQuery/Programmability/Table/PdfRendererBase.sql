IF OBJECT_ID('[dbo].[PdfRendererBase]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfRendererBase] (
		[PRB_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfRendererBase_PdfRendererBaseId] DEFAULT(NEWID()),
		[PRB_Id] VARCHAR(50) NOT NULL,
		[PRB_RendererType] TINYINT NOT NULL,
		[PRB_Margin] VARCHAR(20) NULL,
		[PRB_Padding] VARCHAR(20) NULL,
		[PEB_HorizontalAlignment] TINYINT NULL,
		[PEB_VerticalAlignment] TINYINT NULL,
		[PEB_Position] TINYINT NULL,
		[PEB_Left] FLOAT NULL,
		[PEB_Right] FLOAT NULL,
		[PEB_Top] FLOAT NULL,
		[PEB_Bottom] FLOAT NULL,
		[PEB_FontSize] FLOAT NULL,
		[PEB_FontFamily] VARCHAR(50) NULL,
		[PEB_FontStyle] TINYINT NULL,
		[PEB_Opacity] FLOAT NULL,
		[PEB_BrushColor] TINYINT NULL,
		[PEB_BackgroundColor] TINYINT NULL,
		[PEB_Row] INT NOT NULL,
		[PEB_Column] INT NOT NULL,
		[PEB_RowSpan] INT NULL,
		[PEB_ColumnSpan] INT NULL,

		CONSTRAINT [PK_dbo.PdfRendererBase] PRIMARY KEY CLUSTERED ([PRB_PdfRendererBaseId])
	);
END