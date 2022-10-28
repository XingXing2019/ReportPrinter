using System;
using System.Data;
using System.Linq;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultRedisCacheManager : RedisCacheManagerBase, ISqlResultCacheManager
    {
        private static readonly object _lock = new object();
        
        private static SqlResultRedisCacheManager _instance;
        public static SqlResultRedisCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlResultRedisCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private SqlResultRedisCacheManager() { }

        public void StoreSqlResult(Guid messageId, string sqlId, DataTable sqlResult)
        {
            var procName = $"{GetType().Name}.{nameof(StoreSqlResult)}";

            try
            {
                var key = RedisCacheHelper.CreateRedisKey(GetType().Name, messageId, sqlId);
                var value = RedisCacheHelper.ObjectToByteArray(sqlResult);

                Cache.Set(key, value, Options);
                Logger.Debug($"Store sql result for message: {messageId}, sql: {sqlId} into redis cache", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to store sql result for message: {messageId}, sql: {sqlId} into redis cache. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }

        public bool TryGetSqlResult(Guid messageId, string sqlId, out DataTable sqlResult)
        {
            var procName = $"{GetType().Name}.{nameof(TryGetSqlResult)}";
            sqlResult = null;

            try
            {
                var key = RedisCacheHelper.CreateRedisKey(GetType().Name, messageId, sqlId);
                var value = Cache.Get(key);

                if (value == null)
                {
                    Logger.Debug($"Unable to retrieve sql result from redis cache for message: {messageId}, execute query for sql: {sqlId}", procName);
                    return false;
                }

                Logger.Debug($"Retrieve sql result from redis cache for message: {messageId}, skip executing query for sql: {sqlId}", procName);
                sqlResult = RedisCacheHelper.ByteArrayToObject<DataTable>(value);
                return true;
            }
            catch (Exception)
            {
                Logger.Debug($"Unable to retrieve sql result from redis cache for message: {messageId}, execute query for sql: {sqlId}", procName);
                return false;
            }
        }

        public void RemoveSqlResult(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(RemoveSqlResult)}";

            try
            {
                var db = Connection.GetDatabase();
                var endpoints = Connection.GetEndPoints();
                var pattern = $"{GetType().Name}_{messageId}_*";

                foreach (var endpoint in endpoints)
                {
                    var server = Connection.GetServer(endpoint);
                    var keys = server.Keys(database: db.Database, pattern: pattern).ToArray();
                    db.KeyDelete(keys);
                }

                Logger.Debug($"Remove all sql result for message: {messageId} from redis cache.", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to remove sql result for message: {messageId}, from redis cache. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }

        public new void Reset()
        {
            base.Reset();
        }
    }
}