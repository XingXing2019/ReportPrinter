namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class DeletePdfRendererBaseByIds : StoredProcedureBase
    {
        public DeletePdfRendererBaseByIds(string pdfRendererBaseIds)
        {
            Parameters.Add("@pdfRendererBaseIds", pdfRendererBaseIds);
        }
    }
}