using System.Collections.Generic;
using System;
using System.Data;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlResultCacheManager;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlResultCacheManager
{
    public class SqlResultMemoryCacheManagerTest : CacheTestBase
    {
        private readonly string _fieldName = "_cache";
        private readonly Guid _messageId = Guid.NewGuid();
        private readonly Dictionary<Guid, Dictionary<string, DataTable>> _cache;
        private readonly DataTable _expectedDataTable;
        private readonly string _sqlId = "TestId";

        public SqlResultMemoryCacheManagerTest()
        {
            _cache = GetSqlResultCache();
            _expectedDataTable = GenerateDataTable();
        }

        [Test]
        public void TestStoreSqlResult()
        {
            try
            {
                SqlResultMemoryCacheManager.Instance.StoreSqlResult(_messageId, _sqlId, _expectedDataTable);

                Assert.AreEqual(1, _cache.Count);
                Assert.IsTrue(_cache.ContainsKey(_messageId));
                Assert.IsTrue(_cache[_messageId].ContainsKey(_sqlId));

                var actualDataTable = _cache[_messageId][_sqlId];
                Assert.AreSame(_expectedDataTable, actualDataTable);
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
        public void TestTryGetSqlResult(bool storeData)
        {
            if (storeData)
            {
                SqlResultMemoryCacheManager.Instance.StoreSqlResult(_messageId, _sqlId, _expectedDataTable);
            }

            var expectedCount = storeData ? 1 : 0;
            Assert.AreEqual(expectedCount, _cache.Count);

            try
            {
                var isSuccess = SqlResultMemoryCacheManager.Instance.TryGetSqlResult(_messageId, _sqlId, out var actualDataTable);

                Assert.AreEqual(storeData, isSuccess);
                if (storeData)
                {
                    Assert.AreSame(_expectedDataTable, actualDataTable);
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
        public void TestRemoveSqlResult()
        {
            try
            {
                SqlResultMemoryCacheManager.Instance.StoreSqlResult(_messageId, _sqlId, _expectedDataTable);
                Assert.AreEqual(1, _cache.Count);
                SqlResultMemoryCacheManager.Instance.RemoveSqlResult(_messageId);
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

        private Dictionary<Guid, Dictionary<string, DataTable>> GetSqlResultCache()
        {
            return GetPrivateField<Dictionary<Guid, Dictionary<string, DataTable>>>(SqlResultMemoryCacheManager.Instance, _fieldName);
        }

        #endregion
    }
}