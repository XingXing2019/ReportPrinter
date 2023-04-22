IF OBJECT_ID('[dbo].[GetPdfReprintMarkRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfReprintMarkRenderer]
END
GO

CREATE PROCEDURE [dbo].[GetPdfReprintMarkRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT
		[PRMR_PdfReprintMarkRendererId] AS PdfReprintMarkRendererId,
		[PRMR_PdfRendererBaseId] AS PdfRendererBaseId,
		[PRMR_Text] AS Text,
		[PRMR_BoardThickness] AS BoardThickness,
		[PRMR_Location] AS Location
	FROM
		[PdfReprintMarkRenderer]
	WHERE
		[PRMR_PdfRendererBaseId] = @pdfRendererBaseId		
END