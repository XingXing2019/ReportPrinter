using Microsoft.Extensions.Caching.Distributed;
using ReportPrinterLibrary.Code.Config.Configuration;
using StackExchange.Redis;
using System;
using ReportPrinterLibrary.Code.Log;
using Microsoft.Extensions.Caching.Redis;

namespace RaphaelLibrary.Code.Common
{
    public abstract class RedisCacheManagerBase
    {
        protected readonly RedisConfig Config;
        protected readonly DistributedCacheEntryOptions Options;
        protected readonly IDistributedCache Cache;
        protected readonly IConnectionMultiplexer Connection;

        protected RedisCacheManagerBase()
        {
            Config = AppConfig.Instance.RedisConfig;

            var options = new ConfigurationOptions
            {
                EndPoints = { { Config.Host, Config.Port } },
                AllowAdmin = true
            };

            Connection = ConnectionMultiplexer.Connect(options);

            var optionsAccessor = new RedisCacheOptions
            {
                ConfigurationOptions = options
            };

            Cache = new RedisCache(optionsAccessor);

            TimeSpan? absoluteExpiration = null;
            if (Config.AbsoluteExpirationRelativeToNow.HasValue)
            {
                absoluteExpiration = TimeSpan.FromMinutes(Config.AbsoluteExpirationRelativeToNow.Value);
            }

            TimeSpan? slidingExpiration = null;
            if (Config.SlidingExpiration.HasValue)
            {
                slidingExpiration = TimeSpan.FromMinutes(Config.SlidingExpiration.Value);
            }

            Options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration,
                SlidingExpiration = slidingExpiration
            };
        }

        protected void Reset()
        {
            var procName = $"{GetType().Name}.{nameof(Reset)}";

            try
            {
                var server = Connection.GetServer(Config.Host, Config.Port);
                server.FlushAllDatabases();
                Logger.Debug($"Reset {GetType().Name}", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to reset {GetType().Name}. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }
    }
}