using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer
{
    public class PdfImageRendererSPManager : PdfRendererManagerBase<PdfImageRendererModel>
    {
        public override Task Post(PdfImageRendererModel model)
        {
            throw new NotImplementedException();
        }

        public override Task<PdfImageRendererModel> Get(Guid pdfRendererBaseId)
        {
            throw new NotImplementedException();
        }

        public override Task Put(PdfImageRendererModel model)
        {
            throw new NotImplementedException();
        }
    }
}