using ReportPrinterLibrary.Code.Config.Configuration;
using System;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using ReportPrinterLibrary.Code.Log;
using StackExchange.Redis;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultRedisCacheManager : ISqlResultCacheManager
    {
        private static readonly object _lock = new object();

        private readonly RedisConfig _config;
        private readonly DistributedCacheEntryOptions _options;
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _connection;

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

        private SqlResultRedisCacheManager()
        {
            _config = AppConfig.Instance.RedisConfig;

            var options = new ConfigurationOptions
            {
                EndPoints = { { _config.Host, _config.Port } }
            };

            _connection = ConnectionMultiplexer.Connect(options);

            var optionsAccessor = new RedisCacheOptions
            {
                ConfigurationOptions = options
            };

            _cache = new RedisCache(optionsAccessor);
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow)
            };
        }

        public void StoreSqlResult(Guid messageId, string sqlId, DataTable sqlResult)
        {
            var procName = $"{GetType().Name}.{nameof(StoreSqlResult)}";

            try
            {
                var key = CreateRedisKey(messageId, sqlId);
                var value = RedisCacheHelper.ObjectToByteArray(sqlResult);

                _cache.Set(key, value, _options);
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
                var key = CreateRedisKey(messageId, sqlId);
                var value = _cache.Get(key);

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
                var db = _connection.GetDatabase();
                var endpoints = _connection.GetEndPoints();
                var pattern = $"SqlResultRedisCacheManager_{messageId}_*";

                foreach (var endpoint in endpoints)
                {
                    var server = _connection.GetServer(endpoint);
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

        public void Reset()
        {
            var procName = $"{GetType().Name}.{nameof(Reset)}";

            try
            {
                var server = _connection.GetServer(_config.Host, _config.Port);
                server.FlushAllDatabases();
                Logger.Debug($"Reset SqlResultRedisCacheManager", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to reset SqlResultRedisCacheManager. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }


        #region Helper

        private string CreateRedisKey(Guid messageId, string sqlId)
        {
            return $"SqlResultRedisCacheManager_{messageId}_{sqlId}";
        }

        #endregion
    }
}