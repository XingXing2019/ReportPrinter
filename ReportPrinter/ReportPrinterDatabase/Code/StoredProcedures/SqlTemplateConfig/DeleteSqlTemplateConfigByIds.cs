namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class DeleteSqlTemplateConfigByIds : StoredProcedureBase
    {
        public DeleteSqlTemplateConfigByIds(string sqlTemplateConfigIds)
        {
            Parameters.Add("@sqlTemplateConfigIds", sqlTemplateConfigIds);
        }
    }
}