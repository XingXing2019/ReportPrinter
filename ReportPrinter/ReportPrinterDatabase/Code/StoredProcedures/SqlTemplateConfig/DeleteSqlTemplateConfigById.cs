using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlTemplateConfig
{
    public class DeleteSqlTemplateConfigById : StoredProcedureBase
    {
        public DeleteSqlTemplateConfigById(Guid sqlTemplateConfigId)
        {
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
        }
    }
}