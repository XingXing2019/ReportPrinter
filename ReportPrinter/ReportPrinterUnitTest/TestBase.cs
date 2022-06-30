using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterLibrary.Config.Configuration;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase<T>
    {
        protected IManager<T> Manager;
        protected readonly Dictionary<string, string> ServicePath;

        protected TestBase()
        {
            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);
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
            var sqlVariables = new List<SqlVariable>
            {
                new SqlVariable { Name = "Name1", Value = "Value1" },
                new SqlVariable { Name = "Name2", Value = "Value2" },
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

        #endregion
    }
}