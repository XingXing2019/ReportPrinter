using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlResColumnConfig
{
    public class GetSqlResColumnConfig : StoredProcedureBase
    {
        public GetSqlResColumnConfig(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}