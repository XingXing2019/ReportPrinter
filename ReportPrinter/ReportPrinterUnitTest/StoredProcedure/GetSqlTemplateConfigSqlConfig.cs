using System;
using ReportPrinterDatabase.Code.StoredProcedures;

namespace ReportPrinterUnitTest.StoredProcedure
{
    public class GetSqlTemplateConfigSqlConfig : StoredProcedureBase
    {
        public GetSqlTemplateConfigSqlConfig(Guid sqlConfigId, Guid sqlTemplateConfigId)
        {
            Parameters.Add("@sqlConfigId", sqlConfigId);
            Parameters.Add("@sqlTemplateConfigId", sqlTemplateConfigId);
        }
    }
}