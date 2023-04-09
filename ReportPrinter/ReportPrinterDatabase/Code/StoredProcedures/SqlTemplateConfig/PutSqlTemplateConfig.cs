using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class PutSqlTemplateConfig : StoredProcedureBase
    {
        public PutSqlTemplateConfig(Guid sqlTemplateConfigId, string id, string sqlConfigIds)
        {
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
            Parameters.Add("@id", id);
            Parameters.Add("@sqlConfigIds", sqlConfigIds);
        }
    }
}