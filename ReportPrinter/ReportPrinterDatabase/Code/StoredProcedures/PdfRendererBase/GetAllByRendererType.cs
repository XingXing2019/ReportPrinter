namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class GetAllByRendererType : StoredProcedureBase
    {
        public GetAllByRendererType(byte rendererType)
        {
            Parameters.Add("@rendererType", rendererType);
        }
    }
}