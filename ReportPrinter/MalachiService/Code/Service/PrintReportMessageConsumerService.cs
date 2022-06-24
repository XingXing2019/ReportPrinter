using System;
using System.Threading;
using MalachiService.Code.Configuration;
using MalachiService.Code.Consumer;
using MassTransit;
using ReportPrinterLibrary.Configuration;
using ReportPrinterLibrary.Log;
using Topshelf;

namespace MalachiService.Code.Service
{
    public class PrintReportMessageConsumerService : ServiceControl
    {
        private IBusControl _bus;
        private const string _pdfQueue = "PrintPdfReport";
        private const string _labelQueue = "PrintLabelReport";
        private readonly RabbitMQConfig _rabbitMqConfig;

        public PrintReportMessageConsumerService()
        {
            _rabbitMqConfig = new ConfigReader<RabbitMQConfig>().ReadConfig();
        }

        public bool Start(HostControl hostControl)
        {
            var procName = $"{this.GetType().Name}.{nameof(Start)}";

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username(_rabbitMqConfig.UserName);
                    h.Password(_rabbitMqConfig.Password);
                });

                cfg.ReceiveEndpoint(_pdfQueue, e =>
                {
                    Logger.Info($"MalachiService start listening {_pdfQueue} queue", procName);
                    e.Consumer<PrintPdfReportConsumer>();
                });

                cfg.ReceiveEndpoint(_labelQueue, e =>
                {
                    Logger.Info($"MalachiService start listening {_labelQueue} queue", procName);
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