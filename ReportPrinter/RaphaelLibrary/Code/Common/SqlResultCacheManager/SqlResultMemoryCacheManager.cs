using System;
using System.Collections.Generic;
using System.Data;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultMemoryCacheManager : ISqlResultCacheManager
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<Guid, Dictionary<string, DataTable>> _cache;

        private static SqlResultMemoryCacheManager _instance;
        public static SqlResultMemoryCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SqlResultMemoryCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private SqlResultMemoryCacheManager()
        {
            _cache = new Dictionary<Guid, Dictionary<string, DataTable>>();
        }

        public void StoreSqlResult(Guid messageId, string sqlId, DataTable sqlResult)
        {
            var procName = $"{GetType().Name}.{nameof(StoreSqlResult)}";

            lock (_lock)
            {
                if (!_cache.ContainsKey(messageId))
                    _cache.Add(messageId, new Dictionary<string, DataTable>());

                if (!_cache[messageId].ContainsKey(sqlId))
                {
                    _cache[messageId].Add(sqlId, sqlResult);
                    Logger.Debug($"Store sql result for message: {messageId}, sql: {sqlId} into memory cache. Current cache size: {_cache.Count}", procName);
                }
            }
        }

        public bool TryGetSqlResult(Guid messageId, string sqlId, out DataTable sqlResult)
        {
            var procName = $"{GetType().Name}.{nameof(TryGetSqlResult)}";
            sqlResult = null;

            if (!_cache.ContainsKey(messageId) || !_cache[messageId].ContainsKey(sqlId))
            {
                Logger.Debug($"Unable to retrieve sql result from memory cache for message: {messageId}, execute query for sql: {sqlId}", procName);
                return false;
            }

            Logger.Debug($"Retrieve sql result from memory cache for message: {messageId}, skip executing query for sql: {sqlId}", procName);
            sqlResult = _cache[messageId][sqlId];
            return true;
        }

        public void RemoveSqlResult(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(RemoveSqlResult)}";

            if (_cache.ContainsKey(messageId))
            {
                _cache.Remove(messageId);
                Logger.Debug($"Remove all sql result for message: {messageId} from memory cache. Current cache size: {_cache.Count}", procName);
            }
        }

        public void Reset()
        {
            _instance = new SqlResultMemoryCacheManager();
        }
    }
}