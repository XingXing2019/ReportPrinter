using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportSqlVariable;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage
{
    public class PrintReportMessageSPManager : IPrintReportMessageManager<IPrintReport>
    {
        private readonly StoredProcedureExecutor _executor;

        public PrintReportMessageSPManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var sp = new PostPrintReportMessage(
                    message.MessageId,
                    message.CorrelationId,
                    message.ReportType, 
                    message.TemplateId,
                    message.PrinterId,
                    message.NumberOfCopy,
                    message.HasReprintFlag);

                var spList = new List<StoredProcedureBase> { sp };

                spList.AddRange(message.SqlVariables.Select(x => new PostPrintReportSqlVariable(message.MessageId, x.Name, x.Value)));

                var rows = await _executor.ExecuteNonQueryAsync(spList.ToArray());
                Logger.Debug($"Record message: {message.MessageId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during recording message: {message.MessageId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IPrintReport> Get(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Get)}";

            try
            {
                var message = await _executor.ExecuteQueryOneAsync<Code.Entity.PrintReportMessage>(new GetPrintReportMessage(messageId));
                var sqlVariables = await _executor.ExecuteQueryBatchAsync<Code.Entity.PrintReportSqlVariable>(new GetPrintReportSqlVariable(messageId));

                var res = CreateMessage(message, sqlVariables);
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving message: {messageId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task<IList<IPrintReport>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            try
            {
                var messages = await _executor.ExecuteQueryBatchAsync<Code.Entity.PrintReportMessage>(new GetAllPrintReportMessage());
                var sqlVariables = await _executor.ExecuteQueryBatchAsync<Code.Entity.PrintReportSqlVariable>(new GetAllPrintReportSqlVariable());

                var res = new List<IPrintReport>();

                foreach (var message in messages)
                {
                    var variables = sqlVariables.Where(x => x.MessageId == message.MessageId);
                    res.Add(CreateMessage(message, variables));
                }

                return res;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during retrieving all messages. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeletePrintReportMessageById(messageId));
                Logger.Debug($"Delete message: {messageId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting message: {messageId}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task Delete(List<Guid> messageIds)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeletePrintReportMessageByIds(string.Join(',', messageIds)));
                Logger.Debug($"Delete messages, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting messages. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(DeleteAll)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeleteAllPrintReportMessage());
                Logger.Debug($"Delete all messages, {rows} rows affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting all messages. Ex: {ex.Message}", procName);
                throw;
            }
        }

        public async Task PatchStatus(Guid messageId, MessageStatus status)
        {
            var procName = $"{this.GetType().Name}.{nameof(PatchStatus)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new PatchPrintReportMessageStatus(messageId, status));
                Logger.Debug($"Update status of message: {messageId} to {status}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating status of message: {messageId} to {status}. Ex: {ex.Message}", procName);
                throw;
            }
        }

        #region Helper

        private IPrintReport CreateMessage(Code.Entity.PrintReportMessage entity, IEnumerable<Code.Entity.PrintReportSqlVariable> sqlVariables)
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
            message.SqlVariables = sqlVariables.Select(x => new SqlVariable{Name = x.Name, Value = x.Value}).ToList();

            return message;
        }

        #endregion
    }
}