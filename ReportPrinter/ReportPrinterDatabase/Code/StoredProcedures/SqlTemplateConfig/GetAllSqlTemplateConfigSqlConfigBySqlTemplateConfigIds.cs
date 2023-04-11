namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds : StoredProcedureBase
    {
        public GetAllSqlTemplateConfigSqlConfigBySqlTemplateConfigIds(string sqlTemplateConfigIds)
        {
            Parameters.Add("@sqlTemplateConfigIds", sqlTemplateConfigIds);
        }
    }
}