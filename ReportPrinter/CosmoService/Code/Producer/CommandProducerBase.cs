using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Manager.MessageManager;
using ReportPrinterLibrary.Config.Configuration;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;

namespace CosmoService.Code.Producer
{
    public abstract class CommandProducerBase<T> where T : IMessage
    {
        protected readonly string QueueName;
        protected readonly IMessageManager<T> Manager;
        protected IBusControl Bus;
        private readonly RabbitMQConfig _rabbitMqConfig;

        protected CommandProducerBase(string queueName, IMessageManager<T> manager)
        {
            QueueName = queueName;
            Manager = manager;
            _rabbitMqConfig = AppConfig.Instance.RabbitMQConfig;
            Bus = CreateBus(queueName);
        }

        public async Task ProduceAsync(T message)
        {
            var procName = $"{this.GetType().Name}.{nameof(ProduceAsync)}";
            Logger.Debug($"Start publishing message to queue: {QueueName}", procName);
            Logger.LogJson($"Message content", message, procName);

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await Bus.StartAsync(source.Token);

            try
            {
                await SendMessageAsync(message);
                Logger.Debug($"Success publishing message: {message.MessageId} to queue: {QueueName}", procName);

                await PostMessageAsync(message);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during producing message. Ex: {ex.Message}", procName);
            }
            finally
            {
                await Bus.StopAsync(source.Token);
            }
        }

        protected abstract Task SendMessageAsync(T message);
        protected abstract Task PostMessageAsync(T message);


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