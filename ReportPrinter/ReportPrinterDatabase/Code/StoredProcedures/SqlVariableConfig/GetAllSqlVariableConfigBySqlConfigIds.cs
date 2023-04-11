namespace ReportPrinterDatabase.Code.StoredProcedures.SqlVariableConfig
{
    public class GetAllSqlVariableConfigBySqlConfigIds : StoredProcedureBase
    {
        public GetAllSqlVariableConfigBySqlConfigIds(string sqlConfigIds)
        {
            Parameters.Add("@sqlConfigIds", sqlConfigIds);
        }
    }
}