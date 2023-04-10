namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class GetAllBySqlTemplateIdPrefix : StoredProcedureBase
    {
        public GetAllBySqlTemplateIdPrefix(string sqlTemplateIdPrefix)
        {
            Parameters.Add("@sqlTemplateIdPrefix", sqlTemplateIdPrefix);
        }
    }
}