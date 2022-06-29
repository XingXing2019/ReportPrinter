﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportPrinterDatabase.Context;
using ReportPrinterDatabase.Entity;
using ReportPrinterLibrary.Log;
using ReportPrinterLibrary.RabbitMQ.Message;
using ReportPrinterLibrary.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterDatabase.Manager.MessageManager.PrintReportMessage
{
    public class PrintReportMessageEFCoreManager : IPrintReportMessageManager<IPrintReport>
    {
        public async Task Post(IPrintReport message)
        {
            var procName = $"{this.GetType().Name}.{nameof(Post)}";

            try
            {
                using var context = new ReportPrinterContext();
                var printReportMessage = new Entity.PrintReportMessage
                {
                    MessageId = message.MessageId,
                    CorrelationId = message.CorrelationId,
                    ReportType = message.ReportType.ToString(),
                    TemplateId = message.TemplateId,
                    PrinterId = message.PrinterId,
                    NumberOfCopy = message.NumberOfCopy,
                    HasReprintFlag = message.HasReprintFlag,
                    PublishTime = DateTime.Now,
                    Status = MessageStatus.Publish.ToString()
                };

                var sqlVariables = message.SqlVariables.Select(x => new PrintReportSqlVariable
                {
                    SqlVariableId = Guid.NewGuid(),
                    Message = printReportMessage,
                    Name = x.Name,
                    Value = x.Value
                }).ToList();

                printReportMessage.PrintReportSqlVariables = sqlVariables;
                context.PrintReportMessages.Add(printReportMessage);

                var rows = await context.SaveChangesAsync();
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
                using var context = new ReportPrinterContext();
                var entity = await context.PrintReportMessages
                    .Include(x => x.PrintReportSqlVariables)
                    .FirstOrDefaultAsync(x => x.MessageId == messageId);

                if (entity == null)
                    return null;

                var message = CreateMessage(entity);

                Logger.Debug($"Retrieve message: {messageId}", procName);
                return message;
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
                using var context = new ReportPrinterContext();
                var entities = await context.PrintReportMessages
                    .Include(x => x.PrintReportSqlVariables)
                    .ToListAsync();

                var messages = new List<IPrintReport>();
                foreach (var entity in entities)
                {
                    messages.Add(CreateMessage(entity));
                }

                Logger.Debug($"Retrieve all messages", procName);
                return messages;
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
                using var context = new ReportPrinterContext();
                var entity = await context.PrintReportMessages.FindAsync(messageId);

                if (entity == null)
                {
                    Logger.Debug($"Message: {messageId} does not exist", procName);
                }
                else
                {
                    context.PrintReportMessages.Remove(entity);
                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Delete message: {messageId}, {rows} row affected", procName);
                }
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
                using var context = new ReportPrinterContext();
                context.PrintReportMessages.RemoveRange(context.PrintReportMessages);
                var rows = await context.SaveChangesAsync();
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
                using var context = new ReportPrinterContext();
                var entity = await context.PrintReportMessages.FindAsync(messageId);

                if (entity == null)
                {
                    Logger.Debug($"Message: {messageId} does not exist", procName);
                }
                else if (entity.Status != status.ToString())
                {
                    entity.Status = status.ToString();

                    if (status == MessageStatus.Publish)
                        entity.PublishTime = DateTime.Now;
                    else if (status == MessageStatus.Receive)
                        entity.ReceiveTime = DateTime.Now;
                    else if (status == MessageStatus.Complete)
                        entity.CompleteTime = DateTime.Now;

                    var rows = await context.SaveChangesAsync();
                    Logger.Debug($"Update status of message: {messageId} to {status}, {rows} row affected", procName);
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during updating status of message: {messageId} to {status}. Ex: {ex.Message}", procName);
                throw;
            }
        }


        #region Helper

        private IPrintReport CreateMessage(Entity.PrintReportMessage entity)
        {
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

            message.SqlVariables = entity.PrintReportSqlVariables.Select(x => new SqlVariable
            {
                Name = x.Name,
                Value = x.Value
            }).ToList();

            return message;
        }

        #endregion
    }
}