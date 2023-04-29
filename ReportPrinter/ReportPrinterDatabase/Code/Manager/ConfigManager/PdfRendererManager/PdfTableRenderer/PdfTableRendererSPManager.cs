using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTableRenderer
{
    public class PdfTableRendererSPManager : PdfRendererManagerBase<PdfTableRendererModel>
    {
        public override Task Post(PdfTableRendererModel model)
        {
            throw new NotImplementedException();
        }

        public override Task<PdfTableRendererModel> Get(Guid pdfRendererBaseId)
        {
            throw new NotImplementedException();
        }

        public override Task Put(PdfTableRendererModel model)
        {
            throw new NotImplementedException();
        }
    }
}