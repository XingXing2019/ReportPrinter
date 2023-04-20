IF OBJECT_ID('[dbo].[GetPdfAnnotationRenderer]', 'P') IS NOT NULL
BEGIN
	DROP PROCEDURE [dbo].[GetPdfAnnotationRenderer]
END
GO

CREATE PROCEDURE [dbo].[GetPdfAnnotationRenderer]
	@pdfRendererBaseId UNIQUEIDENTIFIER
AS
BEGIN	
	SELECT
		[PAR_PdfAnnotationRendererId] AS PdfAnnotationRendererId,
		[PAR_PdfRendererBaseId] AS PdfRendererBaseId,
		[PAR_AnnotationRendererType] AS AnnotationRendererType,
		[PAR_Title] AS Title,
		[PAR_Icon] AS Icon,
		[PAR_Content] AS Content,
		[PAR_SqlTemplateId] AS SqlTemplateId,
		[PAR_SqlId] AS SqlId,
		[PAR_SqlResColumn] AS SqlResColumn
	FROM
		[PdfAnnotationRenderer]
	WHERE
		[PAR_PdfRendererBaseId] = @pdfRendererBaseId		
END