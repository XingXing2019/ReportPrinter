using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlResultCacheManager;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlResultCacheManager
{
    public class SqlResultCacheManagerFactoryTest
    {
        [Test]
        [TestCase(CacheManagerType.Memory, typeof(SqlResultMemoryCacheManager))]
        [TestCase(CacheManagerType.Redis, typeof(SqlResultRedisCacheManager))]
        [TestCase((byte)2, null)]
        public void TestCreateSqlResultCacheManager(CacheManagerType managerType, Type expectedType)
        {
            try
            {
                var manager = SqlResultCacheManagerFactory.CreateSqlResultCacheManager(managerType);
                Assert.AreEqual(expectedType, manager.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid type: {managerType} for sql result cache manager";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}