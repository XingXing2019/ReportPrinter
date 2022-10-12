using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase
    {
        protected readonly Dictionary<string, string> ServicePath;
        
        private readonly Random _random;

        protected TestBase()
        {
            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);

            _random = new Random();
        }

        #region Helper

        protected IPrintReport CreateMessage(ReportTypeEnum reportType, bool isValidMessage = true)
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

            reportType = isValidMessage 
                ? reportType 
                : reportType == ReportTypeEnum.Label ? ReportTypeEnum.PDF : ReportTypeEnum.Label;

            var expectedMessage = PrintReportMessageFactory.CreatePrintReportMessage(reportType.ToString());

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

        protected T GetPrivateField<T>(Type objectType, string fieldName, object instance)
        {
            var fieldInfo = objectType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var field = (T)fieldInfo?.GetValue(instance);

            return field;
        }

        #endregion
    }
}