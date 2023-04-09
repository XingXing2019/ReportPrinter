using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class GetSqlConfigsBySqlTemplateConfigId : StoredProcedureBase
    {
        public GetSqlConfigsBySqlTemplateConfigId(Guid sqlTemplateConfigId)
        {
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
        }
    }
}