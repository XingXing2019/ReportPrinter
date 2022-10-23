using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.RabbitMQ
{
    public class RabbitMQTestBase<T> : TestBase
    {
        protected IManager<T> Manager;

        private readonly RabbitMQConfig _rabbitMqConfig;

        public RabbitMQTestBase()
        {
            _rabbitMqConfig = AppConfig.Instance.RabbitMQConfig;
        }

        protected List<object> GetMessages(string queueName, Type messageType)
        {
            var messages = new List<object>();
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfig.Host,
                UserName = _rabbitMqConfig.UserName,
                Password = _rabbitMqConfig.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            while (channel.MessageCount(queueName) != 0)
            {
                var body = channel.BasicGet(queueName, true).Body.ToArray();
                var msg = Encoding.UTF8.GetString(body);

                dynamic obj = JsonConvert.DeserializeObject(msg);
                var message = obj.message.ToObject(messageType);
                messages.Add(message);
            }

            return messages;
        }

    }
}