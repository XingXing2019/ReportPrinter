using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class PostSqlTemplateConfig : StoredProcedureBase
    {
        public PostSqlTemplateConfig(Guid sqlTemplateConfigId, string id, string sqlConfigIds)
        {
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
            Parameters.Add("@id", id);
            Parameters.Add("@sqlConfigIds", sqlConfigIds);
        }
    }
}