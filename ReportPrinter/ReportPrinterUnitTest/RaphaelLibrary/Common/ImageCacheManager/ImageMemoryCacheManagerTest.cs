using System;
using System.Collections.Generic;
using NUnit.Framework;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Common.ImageCacheManager;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.ImageCacheManager
{
    public class ImageMemoryCacheManagerTest : TestBase
    {
        private readonly string _imageSource = @".\RaphaelLibrary\Common\Image\Logo.png";
        private readonly Guid _messageId = Guid.NewGuid();
        private readonly string _fieldName = "_cache";
        private readonly XImage _expectedImage;
        private readonly Dictionary<Guid, Dictionary<string, XImage>> _cache;

        public ImageMemoryCacheManagerTest()
        {
            _expectedImage = XImage.FromFile(_imageSource);
            _cache = GetImageCache();
        }

        [Test]
        public void TestStoreImage()
        {
            try
            {
                global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance.StoreImage(_messageId, _imageSource, _expectedImage);

                Assert.AreEqual(1, _cache.Count);
                Assert.IsTrue(_cache.ContainsKey(_messageId));
                Assert.IsTrue(_cache[_messageId].ContainsKey(_imageSource));

                var actualImage = _cache[_messageId][_imageSource];
                Assert.AreSame(_expectedImage, actualImage);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _cache.Clear();
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetImage(bool storeImage)
        {
            if (storeImage)
            {
                global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance.StoreImage(_messageId, _imageSource, _expectedImage);
            }

            var expectedCount = storeImage ? 1 : 0;
            Assert.AreEqual(expectedCount, _cache.Count);

            try
            {
                var isSuccess = global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance.TryGetImage(_messageId, _imageSource, out var actualImage);

                Assert.AreEqual(storeImage, isSuccess);
                if (storeImage)
                {
                    Assert.AreSame(_expectedImage, actualImage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _cache.Clear();
            }
        }

        [Test]
        public void TestRemoveImage()
        {
            try
            {
                global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance.StoreImage(_messageId, _imageSource, _expectedImage);
                Assert.AreEqual(1, _cache.Count);
                global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance.RemoveImage(_messageId);
                Assert.AreEqual(0, _cache.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _cache.Clear();
            }
        }


        #region Helper

        private Dictionary<Guid, Dictionary<string, XImage>> GetImageCache()
        {
            return GetPrivateField<Dictionary<Guid, Dictionary<string, XImage>>>(global::RaphaelLibrary.Code.Common.ImageCacheManager.ImageMemoryCacheManager.Instance, _fieldName);
        }

        #endregion
    }
}