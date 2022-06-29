using System;
using System.Threading;
using GreenPipes;
using MalachiService.Code.Consumer;
using MassTransit;
using ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Config.Configuration;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.RabbitMQ.MessageQueue;
using Topshelf;

namespace MalachiService.Code.Service
{
    public class PrintReportMessageConsumerService : ServiceControl
    {
        private IBusControl _bus;
        private readonly RabbitMQConfig _rabbitMqConfig;
        private readonly IPrintReportMessageManager<IPrintReport> _manager;

        public PrintReportMessageConsumerService()
        {
            _rabbitMqConfig = AppConfig.Instance.RabbitMQConfig;
            _manager = new PrintReportMessageEFCoreManager();
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
                    e.Consumer(() => new PrintPdfReportConsumer(_manager));
                });

                cfg.ReceiveEndpoint(QueueName.LABEL_QUEUE, e =>
                {
                    Logger.Info($"MalachiService start listening {QueueName.LABEL_QUEUE} queue", procName);
                    e.Consumer(() => new PrintLabelReportConsumer(_manager));
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