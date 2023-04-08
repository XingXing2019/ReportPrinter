IF OBJECT_ID('[dbo].[PostPrintReportSqlVariable]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[PostPrintReportSqlVariable]
END
GO

CREATE PROCEDURE [dbo].[PostPrintReportSqlVariable]
	@messageId UNIQUEIDENTIFIER,
	@name VARCHAR(100),
	@value VARCHAR(100)
AS
BEGIN	
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY

		INSERT INTO [dbo].[PrintReportSqlVariable] (
			[PRSV_SqlVariableId],
			[PRSV_MessageId],
			[PRSV_Name],
			[PRSV_Value]
		) VALUES (
			NEWID(),
			@messageId,
			@name,
			@value
		)

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END