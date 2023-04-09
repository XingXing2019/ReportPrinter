namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class GetAllByDatabaseIdPrefix : StoredProcedureBase
    {
        public GetAllByDatabaseIdPrefix(string databaseIdPrefix)
        {
            Parameters.Add("@databaseIdPrefix", databaseIdPrefix);
        }
    }
}