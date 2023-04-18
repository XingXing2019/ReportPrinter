IF OBJECT_ID('[dbo].[PdfRendererBase]', N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[PdfRendererBase] (
		[PRB_PdfRendererBaseId] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_PdfRendererBase_PdfRendererBaseId] DEFAULT(NEWID()),
		[PRB_Id] VARCHAR(50) NOT NULL,
		[PRB_RendererType] TINYINT NOT NULL,
		[PRB_Margin] VARCHAR(20) NULL,
		[PRB_Padding] VARCHAR(20) NULL,
		[PRB_HorizontalAlignment] TINYINT NULL,
		[PRB_VerticalAlignment] TINYINT NULL,
		[PRB_Position] TINYINT NULL,
		[PRB_Left] FLOAT NULL,
		[PRB_Right] FLOAT NULL,
		[PRB_Top] FLOAT NULL,
		[PRB_Bottom] FLOAT NULL,
		[PRB_FontSize] FLOAT NULL,
		[PRB_FontFamily] VARCHAR(50) NULL,
		[PRB_FontStyle] TINYINT NULL,
		[PRB_Opacity] FLOAT NULL,
		[PRB_BrushColor] TINYINT NULL,
		[PRB_BackgroundColor] TINYINT NULL,
		[PRB_Row] INT NOT NULL,
		[PRB_Column] INT NOT NULL,
		[PRB_RowSpan] INT NULL,
		[PRB_ColumnSpan] INT NULL,

		CONSTRAINT [PK_dbo.PdfRendererBase] PRIMARY KEY CLUSTERED ([PRB_PdfRendererBaseId])
	);
END