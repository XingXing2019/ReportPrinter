using System;
using System.Collections.Generic;
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
            lock (_lock)
            {
                _sqlVariableRepo.Add(messageId, sqlVariables);
            }
        }

        public Dictionary<string, SqlVariable> GetSqlVariables(Guid messageId)
        {
            lock (_lock)
            {
                return _sqlVariableRepo[messageId];
            }
        }

        public void RemoveSqlVariables(Guid messageId)
        {
            lock (_lock)
            {
                _sqlVariableRepo.Remove(messageId);
            }
        }
    }
}