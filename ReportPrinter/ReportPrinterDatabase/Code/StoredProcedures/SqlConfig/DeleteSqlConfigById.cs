using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlConfig
{
    public class DeleteSqlConfigById : StoredProcedureBase
    {
        public DeleteSqlConfigById(Guid sqlConfigId)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
        }
    }
}