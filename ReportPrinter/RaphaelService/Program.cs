using RaphaelService.Code.Service;
using ReportPrinterLibrary.Log;
using Topshelf;

namespace RaphaelService
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var procName = $"RaphaelService.{nameof(Main)}";
            Logger.Info($"RaphaelService start running", procName);

            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(x => new PrintReportMessageConsumerService());
            });
        }
    }
}
