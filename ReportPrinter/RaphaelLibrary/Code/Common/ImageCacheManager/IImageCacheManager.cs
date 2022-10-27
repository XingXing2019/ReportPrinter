using PdfSharp.Drawing;
using System;

namespace RaphaelLibrary.Code.Common.ImageCacheManager
{
    public interface IImageCacheManager
    {
        void StoreImage(Guid messageId, string imageSource, XImage image);

        bool TryGetImage(Guid messageId, string imageSource, out XImage image);

        void RemoveImage(Guid messageId);

        void Reset();
    }
}