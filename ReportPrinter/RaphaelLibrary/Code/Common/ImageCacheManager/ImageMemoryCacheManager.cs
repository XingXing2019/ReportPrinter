using System;
using System.Collections.Generic;
using PdfSharp.Drawing;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common.ImageCacheManager
{
    public class ImageMemoryCacheManager : IImageCacheManager
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<Guid, Dictionary<string, XImage>> _cache;

        private static ImageMemoryCacheManager _instance;
        public static ImageMemoryCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ImageMemoryCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private ImageMemoryCacheManager()
        {
            lock (_lock)
            {
                _cache = new Dictionary<Guid, Dictionary<string, XImage>>();
            }
        }

        public void StoreImage(Guid messageId, string imageSource, XImage image)
        {
            var procName = $"{GetType().Name}.{nameof(StoreImage)}";

            lock (_lock)
            {
                if (!_cache.ContainsKey(messageId))
                    _cache.Add(messageId, new Dictionary<string, XImage>());

                if (!_cache[messageId].ContainsKey(imageSource))
                {
                    _cache[messageId].Add(imageSource, image);
                    Logger.Debug($"Store image for message: {messageId} in memory cache, image source: {imageSource}. Current cache size: {_cache.Count}", procName);
                }
            }
        }

        public bool TryGetImage(Guid messageId, string imageSource, out XImage image)
        {
            var procName = $"{GetType().Name}.{nameof(TryGetImage)}";
            image = null;

            if (!_cache.ContainsKey(messageId) || !_cache[messageId].ContainsKey(imageSource))
            {
                Logger.Debug($"Unable to retrieve image from memory cache for message: {messageId}, image source: {imageSource}", procName);
                return false;
            }

            Logger.Debug($"Retrieve image from memory cache for message: {messageId}, image source: {imageSource}", procName);
            image = _cache[messageId][imageSource];
            return true;
        }

        public void RemoveImage(Guid messageId)
        {
            var procName = $"{GetType().Name}.{nameof(RemoveImage)}";

            if (_cache.ContainsKey(messageId))
            {
                _cache.Remove(messageId);
                Logger.Debug($"Remove all images for message: {messageId} from memory cache. Current cache size: {_cache.Count}", procName);
            }
        }

        public void Reset()
        {
            var procName = $"{GetType().Name}.{nameof(Reset)}";

            _instance = new ImageMemoryCacheManager();
            Logger.Debug($"Reset {GetType().Name}", procName);
        }
    }
}