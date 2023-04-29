using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlResColumnConfig
{
    public class DeleteSqlResColumnConfig : StoredProcedureBase
    {
        public DeleteSqlResColumnConfig(Guid pdfRendererBaseId)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
        }
    }
}