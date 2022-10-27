using ReportPrinterLibrary.Code.Config.Configuration;
using System;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.ImageCacheManager
{
    public class ImageCacheManagerFactory
    {
        public static IImageCacheManager CreateImageCacheManager(CacheManagerType managerType)
        {
            var procName = $"ImageCacheManagerFactory.{nameof(CreateImageCacheManager)}";

            if (managerType == CacheManagerType.Memory)
                return ImageMemoryCacheManager.Instance;
            else if (managerType == CacheManagerType.Redis)
                return ImageRedisCacheManager.Instance;
            else
            {
                var error = $"Invalid type: {managerType} for image cache manager";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}