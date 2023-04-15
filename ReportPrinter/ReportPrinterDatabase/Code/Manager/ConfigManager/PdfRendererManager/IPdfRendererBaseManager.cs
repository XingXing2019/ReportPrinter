using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager
{
    public interface IPdfRendererBaseManager : IManager<PdfRendererBaseModel>
    {
        Task<List<PdfRendererBaseModel>> GetAllByRendererType(PdfRendererType rendererType);
        Task<List<PdfRendererBaseModel>> GetAlByRendererIdPrefix(string rendererIdPrefix);
    }
}