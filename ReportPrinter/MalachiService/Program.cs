using MalachiService.Code.Service;
using Topshelf;
using Logger = ReportPrinterLibrary.Log.Logger;

namespace MalachiService
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var procName = $"MalachiService.{nameof(Main)}";
            Logger.Info($"MalachiService start running", procName);

            return (int)HostFactory.Run(cfg =>
            {
                cfg.Service(x => new PrintReportMessageConsumerService());
            });
        }
    }
}
