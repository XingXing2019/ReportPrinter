using System;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultCacheManagerFactory
    {
        public static ISqlResultCacheManager CreateSqlResultCacheManager(SqlResultCacheManagerType type)
        {
            var procName = $"SqlResultCacheManagerFactory.{nameof(CreateSqlResultCacheManager)}";

            if (type == SqlResultCacheManagerType.Memory)
                return SqlResultMemoryCacheManager.Instance;
            else if (type == SqlResultCacheManagerType.Redis)
                return SqlResultRedisCacheManager.Instance;
            else
            {
                var error = $"Invalid type: {type} for sql result cache manager";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}