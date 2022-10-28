using NUnit.Framework;
using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Threading;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Common.SqlResultCacheManager;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlResultCacheManager
{
    public class SqlResultRedisCacheManagerTest : CacheTestBase
    {
        private readonly Guid _messageId = Guid.NewGuid();
        private readonly string _sqlId = "TestId";
        private readonly DataTable _expectedDataTable;

        public SqlResultRedisCacheManagerTest()
        {
            _expectedDataTable = GenerateDataTable();
        }

        [Test]
        public void TestStoreSqlResult()
        {
            try
            {   
                SqlResultRedisCacheManager.Instance.StoreSqlResult(_messageId, _sqlId, _expectedDataTable);
                
                var key = RedisCacheHelper.CreateRedisKey(nameof(SqlResultRedisCacheManager), _messageId, _sqlId);
                var value = Cache.Get(key);

                Assert.IsNotNull(value);
                var actualDataTable = RedisCacheHelper.ByteArrayToObject<DataTable>(value);

                AssertRedisObject(_expectedDataTable, actualDataTable);

                var expire = Config.AbsoluteExpirationRelativeToNow;
                Thread.Sleep((int)(expire * 60) * 1000);

                value = Cache.Get(key);
                Assert.IsNull(value);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetSqlResult(bool storeData)
        {
            if (storeData)
            {
                var key = RedisCacheHelper.CreateRedisKey(nameof(SqlResultRedisCacheManager), _messageId, _sqlId);
                var value = RedisCacheHelper.ObjectToByteArray(_expectedDataTable);
                Cache.Set(key, value);
            }
            
            try
            {
                var isExist = SqlResultRedisCacheManager.Instance.TryGetSqlResult(_messageId, _sqlId, out var actualTable);
                Assert.AreEqual(storeData, isExist);

                if (storeData)
                {
                    AssertRedisObject(_expectedDataTable, actualTable);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [Test]
        public void TestRemoveSqlResult()
        {
            try
            {
                var extraMessageId = Guid.NewGuid();
                var extraDataTable = GenerateDataTable();
                var extraKey = RedisCacheHelper.CreateRedisKey(nameof(SqlResultRedisCacheManager), extraMessageId, _sqlId);
                var extraValue = RedisCacheHelper.ObjectToByteArray(extraDataTable);
                Cache.Set(extraKey, extraValue);

                var key = RedisCacheHelper.CreateRedisKey(nameof(SqlResultRedisCacheManager), _messageId, _sqlId);
                var value = RedisCacheHelper.ObjectToByteArray(_expectedDataTable);
                Cache.Set(key, value);

                var keys = GetAllRedisKeys();
                Assert.AreEqual(2, keys.Count);
                Assert.IsTrue(keys.Contains(extraKey));
                Assert.IsTrue(keys.Contains(key));

                SqlResultRedisCacheManager.Instance.RemoveSqlResult(_messageId);

                keys = GetAllRedisKeys();
                Assert.AreEqual(1, keys.Count);
                Assert.IsTrue(keys.Contains(extraKey));
                Assert.IsFalse(keys.Contains(key));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestReset()
        {
            try
            {
                var key = RedisCacheHelper.CreateRedisKey(nameof(SqlResultRedisCacheManager), _messageId, _sqlId);
                var value = RedisCacheHelper.ObjectToByteArray(_expectedDataTable);
                Cache.Set(key, value);

                var keys = GetAllRedisKeys();
                Assert.AreEqual(1, keys.Count);
                Assert.IsTrue(keys.Contains(key));

                SqlResultRedisCacheManager.Instance.Reset();

                keys = GetAllRedisKeys();
                Assert.AreEqual(0, keys.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}