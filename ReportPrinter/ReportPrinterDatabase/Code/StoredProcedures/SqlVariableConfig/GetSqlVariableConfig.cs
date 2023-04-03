using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlVariableConfig
{
    public class GetSqlVariableConfig : StoredProcedureBase
    {
        public GetSqlVariableConfig(Guid sqlConfigId)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
        }
    }
}