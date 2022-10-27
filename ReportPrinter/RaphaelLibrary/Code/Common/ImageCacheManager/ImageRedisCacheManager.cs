using System;
using System.Data;
using System.Linq;
using GreenPipes.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Common.SqlResultCacheManager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;
using StackExchange.Redis;

namespace RaphaelLibrary.Code.Common.ImageCacheManager
{
    public class ImageRedisCacheManager : RedisCacheManagerBase, IImageCacheManager
    {
        private static readonly object _lock = new object();
        
        private static ImageRedisCacheManager _instance;
        public static ImageRedisCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ImageRedisCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private ImageRedisCacheManager() { }

        public void StoreImage(Guid messageId, string imageSource, XImage image)
        {
            var procName = $"{GetType().Name}.{nameof(StoreImage)}";

            try
            {
                var key = RedisCacheHelper.CreateRedisKey(GetType().Name, messageId, imageSource);
                var value = RedisCacheHelper.ObjectToByteArray(image);

                Cache.Set(key, value, Options);
                Logger.Debug($"Store image for message: {messageId} in redis cache, image source: {imageSource}.", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to store image for message: {messageId} in redis cache, image source: {imageSource}. Ex: {ex.Message}";
                Logger.Error(error, procName);
                throw new ApplicationException(error);
            }
        }

        public bool TryGetImage(Guid messageId, string imageSource, out XImage image)
        {
            var procName = $"{GetType().Name}.{nameof(TryGetImage)}";
            image = null;

            try
            {
                var key = RedisCacheHelper.CreateRedisKey(GetType().Name, messageId, imageSource);
                var value = Cache.Get(key);

                if (value == null)
                {
                    Logger.Debug($"Unable to retrieve image from redis cache for message: {messageId}, image source: {imageSource}", procName);
                    return false;
                }

                Logger.Debug($"Retrieve image from redis cache for message: {messageId}, image source: {imageSource}", procName);
                image = RedisCacheHelper.ByteArrayToObject<XImage>(value);
                return true;
            }
            catch (Exception)
            {
                Logger.Debug($"Unable to retrieve image from redis cache for message: {messageId}, image source: {imageSource}", procName);
                return false;
            }
        }

        public void RemoveImage(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(RemoveImage)}";

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

                Logger.Debug($"Remove all images for message: {messageId} from redis cache.", procName);
            }
            catch (Exception ex)
            {
                var error = $"Unable to remove images for message: {messageId}, from redis cache. Ex: {ex.Message}";
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