using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using ReportPrinterLibrary.Code.Enum;
using System;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlVariableCacheManager
{
    public class SqlVariableCacheManagerFactoryTest
    {
        [Test]
        [TestCase(CacheManagerType.Memory, typeof(SqlVariableMemoryCacheManager))]
        [TestCase(CacheManagerType.Redis, typeof(SqlVariableRedisCacheManager))]
        [TestCase(2, null)]
        public void TestCreateSqlResultCacheManager(CacheManagerType managerType, Type expectedType)
        {
            try
            {
                var manager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(managerType);
                Assert.AreEqual(expectedType, manager.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid type: {managerType} for sql variable cache manager";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}