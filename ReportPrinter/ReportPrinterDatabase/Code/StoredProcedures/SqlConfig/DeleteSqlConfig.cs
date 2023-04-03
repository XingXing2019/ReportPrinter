using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class DeleteSqlConfig : StoredProcedureBase
    {
        public DeleteSqlConfig(Guid sqlConfigId)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
        }
    }
}