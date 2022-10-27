using System.Collections.Generic;
using System.Data;
using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.Config.Configuration;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class CacheTestBase : TestBase
    {
        protected readonly RedisConfig Config;
        protected readonly IDistributedCache Cache;

        private readonly IConnectionMultiplexer _connection;

        public CacheTestBase()
        {
            Config = AppConfig.Instance.RedisConfig;

            var options = new ConfigurationOptions
            {
                EndPoints = { { Config.Host, Config.Port } },
                AllowAdmin = true
            };

            Cache = new RedisCache(new RedisCacheOptions
            {
                ConfigurationOptions = options
            });

            _connection = ConnectionMultiplexer.Connect(options);
        }

        protected DataTable GenerateDataTable()
        {
            var testDataList = new List<TestData>
            {
                new TestData { Id = Guid.NewGuid(), Value = "Data1" },
                new TestData { Id = Guid.NewGuid(), Value = "Data2" },
            };

            var dataTable = ListToDataTable(testDataList, "TestTable");
            return dataTable;
        }

        protected void AssertRedisObject(object obj1, object obj2)
        {
            var data1 = RedisCacheHelper.ObjectToByteArray(obj1);
            var data2 = RedisCacheHelper.ObjectToByteArray(obj2);

            Assert.AreEqual(data1.Length, data2.Length);
            for (int i = 0; i < data1.Length; i++)
            {
                Assert.AreEqual(data1[i], data2[i]);
            }
        }

        protected void FlushAllDatabases()
        {
            var server = _connection.GetServer(Config.Host, Config.Port);
            server.FlushAllDatabases();
        }

        protected HashSet<string> GetAllRedisKeys()
        {
            var endpoints = _connection.GetEndPoints();

            var keys = new List<RedisKey>();
            foreach (var endpoint in endpoints)
            {
                var server = _connection.GetServer(endpoint);
                keys.AddRange(server.Keys()); 
            }

            return new HashSet<string>(keys.Select(x => x.ToString()));
        }


        #region Helper

        private DataTable ListToDataTable<T>(List<T> list, string tableName)
        {
            var dataTable = new DataTable(tableName);

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (var t in list)
            {
                var row = dataTable.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null) ?? DBNull.Value;
                }
                dataTable.Rows.Add(row);
            }
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