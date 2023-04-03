using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlVariableConfig
{
    public class PostSqlVariableConfig : StoredProcedureBase
    {
        public PostSqlVariableConfig(Guid sqlVariableConfigId, Guid sqlConfigId, string name)
        {
            Parameters.Add("@sqlVariableConfigId", sqlVariableConfigId);
            Parameters.Add("@sqlConfigId", sqlConfigId);
            Parameters.Add("@name", name);
        }
    }
}