IF OBJECT_ID('[dbo].[s_PutPdfTableRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[s_PutPdfTableRenderer]
END
GO

CREATE PROCEDURE [dbo].[s_PutPdfTableRenderer]
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
		
		UPDATE 
			[PdfTableRenderer]
		SET
			[PTR_BoardThickness] = @boardThickness,
			[PTR_LineSpace] = @lineSpace,
			[PTR_TitleHorizontalAlignment] = @titleHorizontalAlignment,
			[PTR_HideTitle] = @hideTitle,
			[PTR_Space] = @space,
			[PTR_TitleColor] = @titleColor,
			[PTR_TitleColorOpacity] = @titleColorOpacity,
			[PTR_SqlTemplateConfigSqlConfigId] = @sqlTemplateConfigSqlConfigId,
			[PTR_SqlVariable] = @sqlVariable,
			[PTR_SubPdfTableRendererId] = @subPdfTableRendererId
		WHERE
			[PTR_PdfRendererBaseId] = @pdfRendererBaseId

		COMMIT TRANSACTION

	END TRY

	BEGIN CATCH
		
		ROLLBACK TRANSACTION
		DECLARE @errorMsg NVARCHAR(2048) = ERROR_MESSAGE()
		RAISERROR(@errorMsg, 16, 1)

	END CATCH
END