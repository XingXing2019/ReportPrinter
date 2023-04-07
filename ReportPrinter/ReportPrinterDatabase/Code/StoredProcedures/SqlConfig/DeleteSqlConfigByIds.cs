namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class DeleteSqlConfigByIds : StoredProcedureBase
    {
        public DeleteSqlConfigByIds(string sqlConfigIds)
        {
            Parameters.Add("@sqlConfigIds", sqlConfigIds);
        }
    }
}