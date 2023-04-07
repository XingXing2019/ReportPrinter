IF OBJECT_ID('PostPrintReportMessage', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE PostPrintReportMessage
END
GO

CREATE PROCEDURE PostPrintReportMessage
	@messageId UNIQUEIDENTIFIER,
	@correlationId UNIQUEIDENTIFIER,
	@reportType VARCHAR(10),
	@templateId VARCHAR(100),
	@printerId VARCHAR(100) = NULL,
	@numberOfCopy INT,
	@hasReprintFlag BIT
AS
BEGIN

	SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	BEGIN TRANSACTION

	BEGIN TRY
		
		INSERT INTO [dbo].[PrintReportMessage] (
			[PRM_MessageId],
			[PRM_CorrelationId],
			[PRM_ReportType],
			[PRM_TemplateId],
			[PRM_PrinterId],
			[PRM_NumberOfCopy],
			[PRM_HasReprintFlag],
			[PRM_PublishTime],
			[PRM_ReceiveTime],
			[PRM_CompleteTime],
			[PRM_Status]
		) VALUES (
			@messageId,
			@correlationId,
			@reportType,
			@templateId,
			@printerId,
			@numberOfCopy,
			@hasReprintFlag,
			GETDATE(),
			NULL,
			NUll,
			'Publish'
		)		

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
	
END