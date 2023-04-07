IF OBJECT_ID('DeletePrintReportMessageByIds', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE DeletePrintReportMessageByIds
END
GO

CREATE PROCEDURE DeletePrintReportMessageByIds
	@messageIds NVARCHAR(MAX)
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END	

	BEGIN TRANSACTION

	BEGIN TRY
	
		DECLARE @temp TABLE (
			MessageId UNIQUEIDENTIFIER
		);
		
		INSERT INTO @temp
		SELECT value FROM STRING_SPLIT(@messageIds, ','); 

		DELETE FROM PrintReportMessage
		WHERE PRM_MessageId IN (
			SELECT * FROM @temp
		)

	COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END