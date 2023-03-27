using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlVariableCacheManager
{
    public class SqlVariableRedisCacheManagerTest : CacheTestBase
    {
        private readonly Guid _messageId;
        private readonly Dictionary<string, SqlVariable> _expectedSqlVariables;

        public SqlVariableRedisCacheManagerTest()
        {
            var message = CreateMessage(ReportTypeEnum.PDF);

            _messageId = message.MessageId;
            _expectedSqlVariables = CreateSqlVariables(message);
        }

        [Test]
        public void TestStoreSqlVariables()
        {
            try
            {
                SqlVariableRedisCacheManager.Instance.StoreSqlVariables(_messageId, _expectedSqlVariables);

                var key = RedisCacheHelper.CreateRedisKey("SqlVariableRedisCacheManager", _messageId);
                var value = Cache.Get(key);
                Assert.IsNotNull(value);

                var actualSqlVariables = RedisCacheHelper.ByteArrayToObject<Dictionary<string, SqlVariable>>(value);
                AssertHelper.AssertObject(_expectedSqlVariables, actualSqlVariables);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestGetSqlVariables(bool storeData)
        {
            if (storeData)
            {
                var key = RedisCacheHelper.CreateRedisKey("SqlVariableRedisCacheManager", _messageId);
                var value = RedisCacheHelper.ObjectToByteArray(_expectedSqlVariables);
                Cache.Set(key, value);
            }

            try
            {
                var actualSqlVariables = SqlVariableRedisCacheManager.Instance.GetSqlVariables(_messageId);
                
                Assert.IsNotNull(actualSqlVariables);
                AssertHelper.AssertObject(_expectedSqlVariables, actualSqlVariables);
            }
            catch (InvalidOperationException ex)
            {
                var errorMsg = $"Cannot convert null into object";
                Assert.AreSame(errorMsg, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestRemoveSqlVariables()
        {
            var key = RedisCacheHelper.CreateRedisKey("SqlVariableRedisCacheManager", _messageId);
            var value = RedisCacheHelper.ObjectToByteArray(_expectedSqlVariables);
            Cache.Set(key, value);

            try
            {
                var bytes = Cache.Get(key);
                Assert.IsNotNull(bytes);

                SqlVariableRedisCacheManager.Instance.RemoveSqlVariables(_messageId);

                bytes = Cache.Get(key);
                Assert.IsNull(bytes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        

        #region Helper

        private Dictionary<string, SqlVariable> CreateSqlVariables(IPrintReport message)
        {
            var sqlVariables = message.SqlVariables.ToDictionary(x => x.Name, x => x);
            return sqlVariables;
        }

        #endregion
    }
}