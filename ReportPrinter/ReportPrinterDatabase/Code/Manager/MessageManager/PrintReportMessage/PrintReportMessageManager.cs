using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.StoredProcedures;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportMessage;
using ReportPrinterDatabase.Code.StoredProcedures.PrintReportSqlVariable;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage
{
    public class PrintReportMessageManager : IPrintReportMessageManager<IPrintReport>
    {
        private readonly StoredProcedureExecutor _executor;

        public PrintReportMessageManager()
        {
            _executor = new StoredProcedureExecutor();
        }

        public async Task Post(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                var spList = new List<StoredProcedureBase>();

                spList.Add(new PostPrintReportMessage(message.MessageId, message.CorrelationId,
                    message.ReportType, message.TemplateId, message.PrinterId, message.NumberOfCopy,
                    message.HasReprintFlag));

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

            throw new NotImplementedException();
        }

        public async Task<IList<IPrintReport>> GetAll()
        {
            var procName = $"{this.GetType().Name}.{nameof(GetAll)}";

            throw new NotImplementedException();
        }

        public async Task Delete(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(Delete)}";

            try
            {
                var rows = await _executor.ExecuteNonQueryAsync(new DeletePrintReportMessage(messageId));
                Logger.Debug($"Delete message: {messageId}, {rows} row affected", procName);
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deleting message: {messageId}. Ex: {ex.Message}", procName);
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
    }
}