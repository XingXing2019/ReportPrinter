using System;
using System.Threading;
using MassTransit;
using RaphaelService.Code.Consumer;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.MessageQueue;
using Topshelf;

namespace RaphaelService.Code.Service
{
    public class PrintReportMessageConsumerService : ServiceControl
    {
        private IBusControl _bus;
        private readonly RabbitMQConfig _rabbitMqConfig;
        private readonly IPrintReportMessageManager<IPrintReport> _manager;

        public PrintReportMessageConsumerService()
        {
            _rabbitMqConfig = AppConfig.Instance.RabbitMQConfig;
            _manager = (IPrintReportMessageManager<IPrintReport>)ManagerFactory.CreateManager<IPrintReport>(typeof(IPrintReportMessageManager<IPrintReport>), AppConfig.Instance.DatabaseManagerType);
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
                    Logger.Info($"RaphaelService start listening {QueueName.PDF_QUEUE} queue", procName);
                    e.Consumer(() => new PrintPdfReportConsumer(_manager));
                });

                cfg.ReceiveEndpoint(QueueName.LABEL_QUEUE, e =>
                {
                    Logger.Info($"RaphaelService start listening {QueueName.LABEL_QUEUE} queue", procName);
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