using ReportPrinterLibrary.Code.Config.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using ReportPrinterLibrary.Code.Log;
using StackExchange.Redis;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultRedisCacheManager : ISqlResultCacheManager
    {
        private static readonly object _lock = new object();
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

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
            var config = AppConfig.Instance.RedisConfig;
            var options = new RedisCacheOptions
            {
                ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { { config.Host, config.Port } }
                }
            };

            _cache = new RedisCache(options);
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(config.AbsoluteExpirationRelativeToNow)
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
                var error = $"Store sql result for message: {messageId}, sql: {sqlId} into redis cache. Ex: {ex.Message}";
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
            
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }


        #region Helper

        private string CreateRedisKey(Guid messageId, string sqlId)
        {
            return $"{messageId}_{sqlId}";
        }

        #endregion
    }
}