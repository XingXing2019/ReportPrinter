using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReportPrinterDatabase.Context;
using ReportPrinterDatabase.Entity;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage
{
    public class EfPrintReportMessageManager : IPrintReportMessageManager
    {
        public async Task Post(IMessage obj)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";
            var message = (IPrintPdfReport)obj;

            using var context = new ReportPrinterContext();
            var printReportMessage = new Entity.PrintReportMessage
            {
                MessageId = message.MessageId,
                CorrelationId = message.CorrelationId,
                TemplateId = message.TemplateId,
                PrinterId = message.PrinterId,
                NumberOfCopy = message.NumberOfCopy,
                HasReprintFlag = message.HasReprintFlag,
                PublishTime = DateTime.Now,
                Status = "Publish"
            };

            var sqlVariables = new List<PrintReportSqlVariable>();
            foreach (var sqlVariable in message.SqlVariables)
            {
                sqlVariables.Add(new PrintReportSqlVariable
                {
                    SqlVariableId = Guid.NewGuid(),
                    Message = printReportMessage,
                    Name = sqlVariable.Name,
                    Value = sqlVariable.Value
                });
            }

            printReportMessage.PrintReportSqlVariables = sqlVariables;
            context.PrintReportMessages.Add(printReportMessage);

            try
            {
                await context.SaveChangesAsync();
                Logger.Debug($"Record message: {message.MessageId} into PrintReportMessage", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during posting messgae into PrintReportMessage. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IMessage> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IMessage>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Put(IMessage obj)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(IMessage obj)
        {
            throw new NotImplementedException();
        }
    }
}