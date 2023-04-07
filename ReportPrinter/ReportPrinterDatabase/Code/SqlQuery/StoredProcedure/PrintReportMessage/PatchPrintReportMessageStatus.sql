IF OBJECT_ID('PatchPrintReportMessageStatus', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PatchPrintReportMessageStatus
END
GO

CREATE PROCEDURE PatchPrintReportMessageStatus
	@messageId UNIQUEIDENTIFIER,
	@status VARCHAR(20)
AS
BEGIN

	SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	BEGIN TRANSACTION

	BEGIN TRY

		UPDATE
			[dbo].[PrintReportMessage]
		SET 
			[PRM_Status] = @status,
			[PRM_PublishTime] = 
			CASE
				WHEN @status = 'Publish' THEN GETDATE() 
				ELSE [PRM_PublishTime]
			END,
			[PRM_ReceiveTime] = 
			CASE 
				WHEN @status = 'Receive' THEN GETDATE() 
				ELSE [PRM_ReceiveTime]
			END,
			[PRM_CompleteTime] = 
			CASE 
				WHEN @status = 'Complete' THEN GETDATE() 
				ELSE [PRM_CompleteTime]
			END
		WHERE 
			[PRM_MessageId] = @messageId		

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
	
END