﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.MessageManager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message;

namespace CosmoService.Code.Producer
{
    public abstract class CommandProducerBase<T> where T : IMessage
    {
        protected readonly string QueueName;
        protected IBusControl Bus;
        
        private readonly IManager<T> _manager;
        private readonly RabbitMQConfig _rabbitMqConfig;

        protected CommandProducerBase(string queueName, IManager<T> manager)
        {
            QueueName = queueName;
            _manager = manager;
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
                await PostMessageAsync(message);
                await SendMessageAsync(message);
                Logger.Debug($"Success publishing message: {message.MessageId} to queue: {QueueName}", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during producing message. Ex: {ex.Message}", procName);
                await DeleteMessageAsync(message.MessageId);
            }
            finally
            {
                await Bus.StopAsync(source.Token);
            }
        }

        protected abstract Task SendMessageAsync(T message);

        private async Task PostMessageAsync(T message)
        {
            await _manager.Post(message);
        }

        private async Task DeleteMessageAsync(Guid messageId)
        {
            await _manager.Delete(messageId);
        }

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