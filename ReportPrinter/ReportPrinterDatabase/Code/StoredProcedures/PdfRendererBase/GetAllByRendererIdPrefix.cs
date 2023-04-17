namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class GetAllByRendererIdPrefix : StoredProcedureBase
    {
        public GetAllByRendererIdPrefix(string @rendererIdPrefix)
        {
            Parameters.Add("@rendererIdPrefix", rendererIdPrefix);
        }
    }
}