using RaphaelLibrary.Code.Common;
using System.Collections.Generic;
using System;
using System.Data;
using NUnit.Framework;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class SqlResultCacheManagerTest : TestBase
    {
        private readonly string _fieldName = "_cache";
        private readonly Guid _messageId = Guid.NewGuid();
        private readonly Dictionary<Guid, Dictionary<string, DataTable>> _cache;
        private readonly DataTable _expectedDataTable;
        private readonly string _id = "TestId";

        public SqlResultCacheManagerTest()
        {
            _cache = GetSqlResultCache();
            _expectedDataTable = GenerateDataTable();
        }

        [Test]
        public void TestStoreSqlResult()
        {
            try
            {
                SqlResultCacheManager.Instance.StoreSqlResult(_messageId, _id, _expectedDataTable);

                Assert.AreEqual(1, _cache.Count);
                Assert.IsTrue(_cache.ContainsKey(_messageId));
                Assert.IsTrue(_cache[_messageId].ContainsKey(_id));

                var actualDataTable = _cache[_messageId][_id];
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
                SqlResultCacheManager.Instance.StoreSqlResult(_messageId, _id, _expectedDataTable);
            }

            var expectedCount = storeData ? 1 : 0;
            Assert.AreEqual(expectedCount, _cache.Count);

            try
            {
                var isSuccess = SqlResultCacheManager.Instance.TryGetSqlResult(_messageId, _id, out var actualDataTable);

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
                SqlResultCacheManager.Instance.StoreSqlResult(_messageId, _id, _expectedDataTable);
                Assert.AreEqual(1, _cache.Count);
                SqlResultCacheManager.Instance.RemoveSqlResult(_messageId);
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
            return GetPrivateField<Dictionary<Guid, Dictionary<string, DataTable>>>(SqlResultCacheManager.Instance, _fieldName);
        }

        private DataTable GenerateDataTable()
        {
            var testDataList = new List<TestData>
            {
                new TestData { Id = Guid.NewGuid(), Value = "Data1" },
                new TestData { Id = Guid.NewGuid(), Value = "Data2" },
            };

            var dataTable = ListToDataTable(testDataList, "TestTable");
            return dataTable;
        }

        private class TestData
        {
            public Guid Id { get; set; }
            public string Value { get; set; }
        }

        #endregion
    }
}