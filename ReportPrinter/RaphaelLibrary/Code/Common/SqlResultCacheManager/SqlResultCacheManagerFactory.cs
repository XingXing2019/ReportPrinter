using System;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.SqlResultCacheManager
{
    public class SqlResultCacheManagerFactory
    {
        public static ISqlResultCacheManager CreateSqlResultCacheManager(CacheManagerType managerType)
        {
            var procName = $"SqlResultCacheManagerFactory.{nameof(CreateSqlResultCacheManager)}";

            if (managerType == CacheManagerType.Memory)
                return SqlResultMemoryCacheManager.Instance;
            else if (managerType == CacheManagerType.Redis)
                return SqlResultRedisCacheManager.Instance;
            else
            {
                var error = $"Invalid type: {managerType} for sql result cache manager";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}