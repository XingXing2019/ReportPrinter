using System;
using ReportPrinterDatabase.Code.Entity;

namespace ReportPrinterDatabase.Code.StoredProcedures.PdfRendererBase
{
    public class PostPdfRendererBase : StoredProcedureBase
    {
        public PostPdfRendererBase(Guid pdfRendererBaseId, string id, byte rendererType, int row, int colum)
        {
            Parameters.Add("@pdfRendererBaseId", pdfRendererBaseId);
            Parameters.Add("@id", id);
            Parameters.Add("@rendererType", rendererType);
            Parameters.Add("@row", row);
            Parameters.Add("@colum", colum);
        }
    }
}