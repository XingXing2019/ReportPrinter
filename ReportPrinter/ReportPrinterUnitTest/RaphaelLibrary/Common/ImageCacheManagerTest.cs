using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Common;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class ImageCacheManagerTest
    {
        private readonly string _imageSource = @".\RaphaelLibrary\Common\Image\Logo.png";
        private readonly Guid _messageId = Guid.NewGuid();

        [Test]
        public void TestStoreImage()
        {
            var expectedImage = XImage.FromFile(_imageSource);
            var cache = GetImageCache();
            Assert.IsNotNull(cache);

            try
            {
                ImageCacheManager.Instance.StoreImage(_messageId, _imageSource, expectedImage);

                Assert.AreEqual(1, cache.Count);
                Assert.IsTrue(cache.ContainsKey(_messageId));
                Assert.IsTrue(cache[_messageId].ContainsKey(_imageSource));

                var actualImage = cache[_messageId][_imageSource];
                Assert.AreSame(expectedImage, actualImage);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                cache.Clear();
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetImage(bool storeImage)
        {
            var expectedImage = XImage.FromFile(_imageSource);
            var cache = GetImageCache();
            Assert.IsNotNull(cache);

            if (storeImage)
            {
                ImageCacheManager.Instance.StoreImage(_messageId, _imageSource, expectedImage);
            }

            try
            {
                var isSuccess = ImageCacheManager.Instance.TryGetImage(_messageId, _imageSource, out var actualImage);

                Assert.AreEqual(storeImage, isSuccess);
                if (storeImage)
                {
                    Assert.AreSame(expectedImage, actualImage);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                cache.Clear();
            }
        }

        [Test]
        public void TestRemoveImage()
        {
            var expectedImage = XImage.FromFile(_imageSource);
            var cache = GetImageCache();
            cache.Add(_messageId, new Dictionary<string, XImage>());
            cache[_messageId].Add(_imageSource, expectedImage);

            try
            {
                Assert.AreEqual(1, cache.Count);
                ImageCacheManager.Instance.RemoveImage(_messageId);
                Assert.AreEqual(0, cache.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                cache.Clear();
            }
        }


        #region Helper

        Dictionary<Guid, Dictionary<string, XImage>> GetImageCache()
        {
            var type = typeof(ImageCacheManager);
            var fieldInfo = type.GetField("_cache", BindingFlags.NonPublic | BindingFlags.Instance);
            var cache = fieldInfo?.GetValue(ImageCacheManager.Instance) as Dictionary<Guid, Dictionary<string, XImage>>;

            return cache;
        }

        #endregion
    }
}