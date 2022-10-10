using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using MassTransit.Transports;
using Newtonsoft.Json;
using NUnit.Framework;
using RabbitMQ.Client;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase<T>
    {
        protected IManager<T> Manager;
        protected readonly Dictionary<string, string> ServicePath;

        private readonly RabbitMQConfig _rabbitMqConfig;
        private readonly Random _random;

        protected TestBase()
        {
            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);

            _rabbitMqConfig = AppConfig.Instance.RabbitMQConfig;
            _random = new Random();
        }

        #region Helper

        protected IPrintReport CreateMessage(string reportType)
        {
            var messageId = Guid.NewGuid();
            var correlationId = Guid.NewGuid();
            var templateId = "Template1";
            var printerId = "Printer1";
            var numberOfCopy = 3;
            var hasReprintFlag = true;

            var index = _random.Next(100);
            var sqlVariables = new List<SqlVariable>
            {
                new SqlVariable { Name = $"Name{index}", Value = $"Value{index}" },
                new SqlVariable { Name = $"Name{index + 1}", Value = $"Value{index + 1}" },
            };

            var expectedMessage = PrintReportMessageFactory.CreatePrintReportMessage(reportType);

            expectedMessage.MessageId = messageId;
            expectedMessage.CorrelationId = correlationId;
            expectedMessage.TemplateId = templateId;
            expectedMessage.PrinterId = printerId;
            expectedMessage.NumberOfCopy = numberOfCopy;
            expectedMessage.HasReprintFlag = hasReprintFlag;
            expectedMessage.SqlVariables = sqlVariables;

            return expectedMessage;
        }

        protected void AssetMessage(IPrintReport expected, IPrintReport actual)
        {
            Assert.AreEqual(expected.MessageId, actual.MessageId);
            Assert.AreEqual(expected.CorrelationId, actual.CorrelationId);
            Assert.AreEqual(expected.ReportType, actual.ReportType);
            Assert.AreEqual(expected.TemplateId, actual.TemplateId);
            Assert.AreEqual(expected.PrinterId, actual.PrinterId);
            Assert.AreEqual(expected.NumberOfCopy, actual.NumberOfCopy);
            Assert.AreEqual(expected.HasReprintFlag, actual.HasReprintFlag);

            Assert.AreEqual(expected.SqlVariables.Count, actual.SqlVariables.Count);
            foreach (var variable in expected.SqlVariables)
            {
                Assert.IsTrue(actual.SqlVariables.Any(x => x.Name == variable.Name && x.Value == variable.Value));
            }
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

        #endregion
    }
}