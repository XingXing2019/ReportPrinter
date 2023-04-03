using System;
using System.Collections.Generic;
using System.Linq;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.ReportPrinterDatabase
{
    public class DatabaseTestBase<T> : TestBase
    {
        protected IManager<T> Manager;

        protected IPrintReport CreateMessage(PrintReportMessage entity, IEnumerable<PrintReportSqlVariable> sqlVariables)
        {
            if (entity == null)
                return null;

            var message = PrintReportMessageFactory.CreatePrintReportMessage(entity.ReportType);

            message.MessageId = entity.MessageId;
            message.CorrelationId = entity.CorrelationId;
            message.TemplateId = entity.TemplateId;
            message.PrinterId = entity.PrinterId;
            message.NumberOfCopy = entity.NumberOfCopy;
            message.HasReprintFlag = entity.HasReprintFlag;
            message.PublishTime = entity.PublishTime;
            message.ReceiveTime = entity.ReceiveTime;
            message.CompleteTime = entity.CompleteTime;
            message.Status = entity.Status;
            message.SqlVariables = sqlVariables.Select(x => new SqlVariable { Name = x.Name, Value = x.Value }).ToList();

            return message;
        }

        protected SqlConfig CreateSqlConfig(string id)
        {
            var sqlConfigId = Guid.NewGuid();
            return new SqlConfig
            {
                SqlConfigId = sqlConfigId,
                Id = id,
                DatabaseId = "Test DB",
                Query = $"SELECT * FROM SqlConfig WHERE SC_ID = {id}",
                SqlVariableConfigs = new List<SqlVariableConfig>
                {
                    new SqlVariableConfig { SqlVariableConfigId = Guid.NewGuid(), SqlConfigId = sqlConfigId, Name = "Test Name 1" },
                    new SqlVariableConfig { SqlVariableConfigId = Guid.NewGuid(), SqlConfigId = sqlConfigId, Name = "Test Name 2" },
                    new SqlVariableConfig { SqlVariableConfigId = Guid.NewGuid(), SqlConfigId = sqlConfigId, Name = "Test Name 3" }
                }
            };
        }
    }
}