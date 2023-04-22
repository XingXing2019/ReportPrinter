using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer
{
    public class PdfReprintMarkRendererSPManager : PdfRendererManagerBase<PdfReprintMarkRendererModel, Entity.PdfReprintMarkRenderer>
    {
        public override Task Post(PdfReprintMarkRendererModel model)
        {
            throw new NotImplementedException();
        }

        public override Task<PdfReprintMarkRendererModel> Get(Guid pdfRendererBaseId)
        {
            throw new NotImplementedException();
        }

        public override Task Put(PdfReprintMarkRendererModel model)
        {
            throw new NotImplementedException();
        }
    }
}