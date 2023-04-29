using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf.Annotations;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
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

        protected SqlConfig CreateSqlConfig(Guid sqlConfigId, string id, string databaseId, string query, List<string> sqlVariableNames)
        {
            var sqlVariableConfigs = sqlVariableNames.Select(x => new SqlVariableConfig { SqlConfigId = sqlConfigId, Name = x }).ToList();

            return new SqlConfig
            {
                SqlConfigId = sqlConfigId,
                Id = id,
                DatabaseId = databaseId,
                Query = query,
                SqlVariableConfigs = sqlVariableConfigs
            };
        }

        protected SqlTemplateConfigModel CreateSqlTemplateConfig(Guid sqlTemplateConfigId, List<SqlConfig> sqlConfigs, string sqlTemplateId)
        {
            return new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = sqlTemplateConfigId,
                Id = sqlTemplateId,
                SqlConfigs = sqlConfigs
            };
        }
    }
}