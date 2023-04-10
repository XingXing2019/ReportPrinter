namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class GetAllSqlConfigBySqlTemplateConfigIds : StoredProcedureBase
    {
        public GetAllSqlConfigBySqlTemplateConfigIds(string sqlTemplateConfigIds)
        {
            Parameters.Add("@sqlTemplateConfigIds", sqlTemplateConfigIds);
        }
    }
}