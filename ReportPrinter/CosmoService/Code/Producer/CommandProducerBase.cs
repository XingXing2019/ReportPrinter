using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using ReportPrinterLibrary.Config.Configuration;
using ReportPrinterLibrary.Config.Helper;
using ReportPrinterLibrary.Log;

namespace CosmoService.Code.Producer
{
    public abstract class CommandProducerBase
    {
        protected readonly string QueueName;
        protected IBusControl Bus;
        private readonly RabbitMQConfig _rabbitMqConfig;

        protected CommandProducerBase(string queueName)
        {
            QueueName = queueName;
            _rabbitMqConfig = ConfigReader<RabbitMQConfig>.ReadConfig();
            Bus = CreateBus(queueName);
        }

        public async Task Produce(object message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Produce)}";
            Logger.Debug($"Start publishing message to queue: {QueueName}", procName);
            Logger.LogJson($"Message content", message, procName);

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await Bus.StartAsync(source.Token);

            try
            {
                await SendMessage(message);
                Logger.Debug($"Success publishing message to queue: {QueueName}", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during sending message. Ex: {ex.Message}", procName);
            }
            finally
            {
                await Bus.StopAsync(source.Token);
            }
        }

        protected abstract Task SendMessage(object message);


        #region Helper

        private IBusControl CreateBus(string queueName)
        {
            var bus = MassTransit.Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(_rabbitMqConfig.Host, _rabbitMqConfig.VirtualHost, h =>
                {
                    h.Username(_rabbitMqConfig.UserName);
                    h.Password(_rabbitMqConfig.Password);
                });
            });

            return bus;
        }

        #endregion
    }
}