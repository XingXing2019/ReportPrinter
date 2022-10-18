using System;
using System.Collections.Generic;
using PdfSharp.Drawing;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common
{
    public class ImageCacheManager
    {
        private static readonly object _lock = new object();
        private readonly Dictionary<Guid, Dictionary<string, XImage>> _cache;

        private static ImageCacheManager _instance;
        public static ImageCacheManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ImageCacheManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private ImageCacheManager()
        {
            lock (_lock)
            {
                _cache = new Dictionary<Guid, Dictionary<string, XImage>>();
            }
        }

        public void StoreImage(Guid messageId, string imageSource, XImage image)
        {
            var procName = $"{this.GetType().Name}.{nameof(StoreImage)}";

            lock (_lock)
            {
                if (!_cache.ContainsKey(messageId))
                    _cache.Add(messageId, new Dictionary<string, XImage>());

                if (!_cache[messageId].ContainsKey(imageSource))
                {
                    _cache[messageId].Add(imageSource, image);
                    Logger.Debug($"Store image for message: {messageId}, image source: {imageSource}. Current cache size: {_cache.Count}", procName);
                }
            }
        }

        public bool TryGetImage(Guid messageId, string imageSource, out XImage image)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetImage)}";
            image = null;

            if (!_cache.ContainsKey(messageId) || !_cache[messageId].ContainsKey(imageSource))
            {
                Logger.Debug($"Unable to retrieve image from cache for message: {messageId}, image source: {imageSource}", procName);
                return false;
            }

            Logger.Debug($"Retrieve image from cache for message: {messageId}, image source: {imageSource}", procName);
            image = _cache[messageId][imageSource];
            return true;
        }

        public void RemoveImage(Guid messageId)
        {
            var procName = $"{this.GetType().Name}.{nameof(RemoveImage)}";

            if (_cache.ContainsKey(messageId))
            {
                _cache.Remove(messageId);
                Logger.Debug($"Remove all images for message: {messageId} from cache. Current cache size: {_cache.Count}", procName);
            }
        }

        public void Reset()
        {
            _instance = new ImageCacheManager();
        }
    }
}