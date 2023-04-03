using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class GetSqlConfig : StoredProcedureBase
    {
        public GetSqlConfig(Guid sqlConfigId)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
        }
    }
}