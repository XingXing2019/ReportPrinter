using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class PutSqlConfig : StoredProcedureBase
    {
        public PutSqlConfig(Guid sqlConfigId, string id, string databaseId, string query, string sqlVariableNames)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
            Parameters.Add("@id", id);
            Parameters.Add("@databaseId", databaseId);
            Parameters.Add("@query", query);
            Parameters.Add("@sqlVariableNames", sqlVariableNames);
        }
    }
}