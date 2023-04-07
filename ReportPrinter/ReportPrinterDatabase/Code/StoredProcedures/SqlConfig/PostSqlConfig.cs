using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class PostSqlConfig : StoredProcedureBase
    {
        public PostSqlConfig(Guid sqlConfigId, string id, string databaseId, string query, string @sqlVariableNames)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
            Parameters.Add("@id", id);
            Parameters.Add("@databaseId", databaseId);
            Parameters.Add("@query", query);
            Parameters.Add("@sqlVariableNames", sqlVariableNames);
        }
    }
}