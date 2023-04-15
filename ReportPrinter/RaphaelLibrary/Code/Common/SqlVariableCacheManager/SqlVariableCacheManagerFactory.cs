using System;
using ReportPrinterLibrary.Code.Log;
using ReportPrinterLibrary.Code.Enum;

namespace RaphaelLibrary.Code.Common.SqlVariableCacheManager
{
    public class SqlVariableCacheManagerFactory
    {
        public static ISqlVariableCacheManager CreateSqlVariableCacheManager(CacheManagerType managerType)
        {
            var procName = $"SqlVariableCacheManagerFactory.{nameof(CreateSqlVariableCacheManager)}";

            if (managerType == CacheManagerType.Memory)
                return SqlVariableMemoryCacheManager.Instance;
            else if (managerType == CacheManagerType.Redis)
                return SqlVariableRedisCacheManager.Instance;
            else
            {
                var error = $"Invalid type: {managerType} for sql variable cache manager";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}