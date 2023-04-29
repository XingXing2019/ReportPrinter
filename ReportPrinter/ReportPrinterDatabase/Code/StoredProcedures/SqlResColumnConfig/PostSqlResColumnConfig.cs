using System;

namespace ReportPrinterDatabase.Code.StoredProcedures.SqlResColumnConfig
{
    public class PostSqlResColumnConfig : StoredProcedureBase
    {
        public PostSqlResColumnConfig(Guid pdfRendererBaseId, string id, string title, double? widthRatio, byte? position, double? left, double? right)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@id", id);
            Parameters.Add("@title", title);
            Parameters.Add("@widthRatio", widthRatio);
            Parameters.Add("@position", position);
            Parameters.Add("@left", left);
            Parameters.Add("@right", right);
        }
    }
}