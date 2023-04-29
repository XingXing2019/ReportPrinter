IF OBJECT_ID('[dbo].[s_PostSqlResColumnConfig]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostSqlResColumnConfig]
END
GO

CREATE PROCEDURE [dbo].[s_PostSqlResColumnConfig]
	@pdfRendererBaseId UNIQUEIDENTIFIER,
	@id VARCHAR(50),
	@title VARCHAR(50),
	@widthRatio FLOAT,
	@position TINYINT,
	@left FLOAT,
	@right FLOAT
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
		
		INSERT INTO [SqlResColumnConfig] (
			[SRCC_PdfRendererBaseId],
			[SRCC_Id],
			[SRCC_Title],
			[SRCC_WidthRatio],
			[SRCC_Position],
			[SRCC_Left],
			[SRCC_Right]
		) VALUES (
			@pdfRendererBaseId,
			@id,
			@title,
			@widthRatio,
			@position,
			@left,
			@right
		)		

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END