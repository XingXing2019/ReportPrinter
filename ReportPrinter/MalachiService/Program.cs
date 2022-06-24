using MalachiService.Code.Service;
using Topshelf;
using Logger = ReportPrinterLibrary.Log.Logger;

namespace MalachiService
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Logger.Info($"MalachiService start running");

            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(x => new PrintReportMessageConsumerService());
            });
        }
    }
}
