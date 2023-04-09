using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class GetSqlTemplateConfig : StoredProcedureBase
    {
        public GetSqlTemplateConfig(Guid sqlTemplateConfigId)
        {
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
        }
    }
}