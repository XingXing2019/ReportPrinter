using RaphaelLibrary.Code.Init;
using RaphaelService.Code.Service;
using ReportPrinterLibrary.Code.Log;
using Topshelf;

namespace RaphaelService
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var procName = $"RaphaelService.{nameof(Main)}";
            Logger.Info($"RaphaelService start running", procName);
            
            if (!new AppInitializer().Execute())
            {
                return -1;
            }
            
            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(x => new PrintReportMessageConsumerService());
            });
        }
    }
}
