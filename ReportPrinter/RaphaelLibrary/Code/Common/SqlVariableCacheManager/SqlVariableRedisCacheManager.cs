using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Common.SqlVariableCacheManager
{
    public class SqlVariableRedisCacheManager : RedisCacheManagerBase, ISqlVariableCacheManager
    {
        private static readonly object _lock = new object();

        private static SqlVariableRedisCacheManager _instance;
        public static SqlVariableRedisCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlVariableRedisCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private SqlVariableRedisCacheManager() { }

        public void StoreSqlVariables(Guid messageId, Dictionary<string, SqlVariable> sqlVariables)
        {
            var procName = $"{GetType().Name}.{nameof(StoreSqlVariables)}";

            try
            {
                var key = messageId.ToString();
                var value = RedisCacheHelper.ObjectToByteArray(sqlVariables);

                Cache.Set(key, value, Options);
                Logger.Debug($"Store sql variables for message: {messageId}.", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to store sql variables for message: {messageId}. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }

        public Dictionary<string, SqlVariable> GetSqlVariables(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(GetSqlVariables)}";

            var key = messageId.ToString();
            var value = Cache.Get(key);
            var variables = RedisCacheHelper.ByteArrayToObject<Dictionary<string, SqlVariable>>(value);

            Logger.Debug($"Get sql variables for message: {messageId}", procName);
            return variables;
        }

        public void RemoveSqlVariables(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(RemoveSqlVariables)}";

            var key = messageId.ToString();
            Cache.Remove(key);
            Logger.Debug($"Remove sql variables for message: {messageId}.", procName);
        }

        public new void Reset()
        {
            base.Reset();
        }
    }
}