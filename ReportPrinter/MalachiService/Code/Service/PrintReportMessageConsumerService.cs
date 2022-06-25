using System;
using System.Threading;
using MalachiService.Code.Config;
using MalachiService.Code.Consumer;
using MassTransit;
using ReportPrinterLibrary.Config.Helper;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;
using Topshelf;

namespace MalachiService.Code.Service
{
    public class PrintReportMessageConsumerService : ServiceControl
    {
        private IBusControl _bus;
        private readonly RabbitMQConfig _rabbitMqConfig;

        public PrintReportMessageConsumerService()
        {
            _rabbitMqConfig = ConfigReader<RabbitMQConfig>.ReadConfig();
        }

        public bool Start(HostControl hostControl)
        {
            var procName = $"{this.GetType().Name}.{nameof(Start)}";

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(_rabbitMqConfig.Host, _rabbitMqConfig.VirtualHost, h =>
                {
                    h.Username(_rabbitMqConfig.UserName);
                    h.Password(_rabbitMqConfig.Password);
                });

                cfg.ReceiveEndpoint(QueueName.PDF_QUEUE, e =>
                {
                    Logger.Info($"MalachiService start listening {QueueName.PDF_QUEUE} queue", procName);
                    e.Consumer<PrintPdfReportConsumer>();
                });

                cfg.ReceiveEndpoint(QueueName.LABEL_QUEUE, e =>
                {
                    Logger.Info($"MalachiService start listening {QueueName.LABEL_QUEUE} queue", procName);
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