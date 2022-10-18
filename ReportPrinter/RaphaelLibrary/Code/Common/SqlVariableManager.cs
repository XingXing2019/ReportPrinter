using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Common
{
    public class SqlVariableManager
    {
        private static readonly object _lock = new object();
        private Dictionary<Guid, Dictionary<string, SqlVariable>> _sqlVariableRepo;

        private static SqlVariableManager _instance;
        public static SqlVariableManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlVariableManager();
                        }
                    }
                }

                return _instance;
            }
        }

        public SqlVariableManager()
        {
            lock (_lock)
            {
                _sqlVariableRepo = new Dictionary<Guid, Dictionary<string, SqlVariable>>();
            }
        }

        public void StoreSqlVariables(Guid messageId, Dictionary<string, SqlVariable> sqlVariables)
        {
            var procName = $"{this.GetType().Name}.{nameof(StoreSqlVariables)}";

            lock (_lock)
            {
                _sqlVariableRepo.Add(messageId, sqlVariables);
                Logger.Debug($"Store sql variables for message: {messageId}. Current variable repo size: {_sqlVariableRepo.Count}", procName);
            }
        }

        public Dictionary<string, SqlVariable> GetSqlVariables(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(GetSqlVariables)}";
            
            Logger.Debug($"Get sql variables for message: {messageId}", procName);
            return _sqlVariableRepo[messageId];
        }

        public void RemoveSqlVariables(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(RemoveSqlVariables)}";

            if (_sqlVariableRepo.ContainsKey(messageId))
            {
                _sqlVariableRepo.Remove(messageId);
                Logger.Debug($"Remove sql variables for message: {messageId}. Current variable repo size: {_sqlVariableRepo.Count}", procName);
            }
        }

        public void Reset()
        {
            _instance = new SqlVariableManager();
        }
    }
}