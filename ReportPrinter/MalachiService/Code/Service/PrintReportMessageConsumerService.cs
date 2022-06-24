using System;
using System.Threading;
using MalachiService.Code.Consumer;
using MassTransit;
using ReportPrinterLibrary.Log;
using Topshelf;

namespace MalachiService.Code.Service
{
    public class PrintReportMessageConsumerService : ServiceControl
    {
        private IBusControl _bus;
        private const string _pdfQueue = "PrintPdfReport";
        private const string _labelQueue = "PrintLabelReport";

        public bool Start(HostControl hostControl)
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(_pdfQueue, e =>
                {
                    Logger.Info($"MalachiService start listening {_pdfQueue} queue");
                    e.Consumer<PrintPdfReportConsumer>();
                });

                cfg.ReceiveEndpoint(_labelQueue, e =>
                {
                    Logger.Info($"MalachiService start listening {_labelQueue} queue");
                    e.Consumer<PrintLabelReportConsumer>();
                });
            });

            _bus.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _bus.StopAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            return true;
        }
    }
}