IF OBJECT_ID('[dbo].[s_PostPdfTableRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PostPdfTableRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PostPdfTableRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER,
	@boardThickness FLOAT,
	@lineSpace FLOAT,
	@titleHorizontalAlignment TINYINT,
	@hideTitle BIT,
	@space FLOAT,
	@titleColor TINYINT,
	@titleColorOpacity FLOAT,
	@sqlTemplateConfigSqlConfigId UNIQUEIDENTIFIER,
	@sqlVariable VARCHAR(50),
	@subPdfTableRendererId UNIQUEIDENTIFIER
AS
BEGIN
	IF @@TRANCOUNT = 0
	BEGIN
		SET TRANSACTION ISOLATION LEVEL SNAPSHOT
	END

	BEGIN TRANSACTION

	BEGIN TRY
		
		INSERT INTO [PdfTableRenderer] (
			[PTR_PdfRendererBaseId],
			[PTR_BoardThickness],
			[PTR_LineSpace],
			[PTR_TitleHorizontalAlignment],
			[PTR_HideTitle],
			[PTR_Space],
			[PTR_TitleColor],
			[PTR_TitleColorOpacity],
			[PTR_SqlTemplateConfigSqlConfigId],
			[PTR_SqlVariable],
			[PTR_SubPdfTableRendererId]
		) VALUES (
			@pdfRendererBaseId,@boardThickness,
			@lineSpace,
			@titleHorizontalAlignment,
			@hideTitle,
			@space,
			@titleColor,
			@titleColorOpacity,
			@sqlTemplateConfigSqlConfigId,
			@sqlVariable,
			@subPdfTableRendererId
		)		

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END