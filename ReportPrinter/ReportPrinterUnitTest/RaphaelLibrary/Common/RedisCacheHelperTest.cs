using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class RedisCacheHelperTest : CacheTestBase
    {
        [Test]
        public void TestObjectToByteArray()
        {
            try
            {
                var obj = AppConfig.Instance;
                var bytes = RedisCacheHelper.ObjectToByteArray(obj);

                Assert.IsNotNull(bytes);
                Assert.IsTrue(bytes.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestByteArrayToObject()
        {
            try
            {
                var expectedObj = AppConfig.Instance;
                var bytes = RedisCacheHelper.ObjectToByteArray(expectedObj);

                Assert.IsNotNull(bytes);
                Assert.IsTrue(bytes.Length > 0);

                var actualObj = RedisCacheHelper.ByteArrayToObject<AppConfig>(bytes);

                AssertObject(expectedObj, actualObj);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private class TestClass
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private class SubTestClass
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
        }
    }
}