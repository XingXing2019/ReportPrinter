﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class SqlConfigManagerTest : DatabaseTestBase<SqlConfig>
    {
        public SqlConfigManagerTest()
        {
            Manager = new SqlConfigEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll();
        }


        [Test]
        [TestCase(typeof(SqlConfigEFCoreManager))]
        [TestCase(typeof(SqlConfigSPManager))]
        public async Task TestSqlConfigManager_Get(Type managerType)
        {
            try
            {
                var mgr = (ISqlConfigManager)Activator.CreateInstance(managerType);
                
                var sqlConfigId = Guid.NewGuid();
                var id = "Test Sql Config 1";
                var databaseId = "Test DB 1";
                var query = "Test Query 1";
                var sqlVariableNames = new List<string> { "Variable 1", "Variable 2", "Variable 3" };
                var expectedSqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                
                await mgr.Post(expectedSqlConfig);

                var actualSqlConfig = await mgr.Get(sqlConfigId);
                Assert.NotNull(actualSqlConfig);
                AssertSqlConfig(expectedSqlConfig, actualSqlConfig);

                id = "Test Sql Config 2";
                databaseId = "Test DB 2";
                query = "Test Query 2";
                sqlVariableNames = new List<string> { "Variable A", "Variable B", "Variable C" };
                expectedSqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);

                await mgr.PutSqlConfig(expectedSqlConfig);

                actualSqlConfig = await mgr.Get(sqlConfigId);
                Assert.NotNull(actualSqlConfig);
                AssertSqlConfig(expectedSqlConfig, actualSqlConfig);

                await mgr.Delete(sqlConfigId);
                actualSqlConfig = await mgr.Get(sqlConfigId);
                Assert.IsNull(actualSqlConfig);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(SqlConfigEFCoreManager))]
        [TestCase(typeof(SqlConfigSPManager))]
        public async Task TestSqlConfigManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (ISqlConfigManager)Activator.CreateInstance(managerType);
                var expectedSqlConfigs = new List<SqlConfig>();

                for (int i = 0; i < 10; i++)
                {
                    var sqlConfigId = Guid.NewGuid();
                    var id = $"Test Sql Config {i + 1}";
                    var databaseId = $"Test DB {i + 1}";
                    var query = $"Test Query {i + 1}";
                    var sqlVariableNames = new List<string> { $"Variable {i + 1}" };

                    var expectedSqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                    await mgr.Post(expectedSqlConfig);
                    expectedSqlConfigs.Add(expectedSqlConfig);
                }
                
                var actualSqlConfigs = await mgr.GetAll();
                Assert.AreEqual(10, actualSqlConfigs.Count);

                foreach (var expectedSqlConfig in expectedSqlConfigs)
                {
                    var actualSqlConfig = actualSqlConfigs.First(x => x.SqlConfigId == expectedSqlConfig.SqlConfigId);
                    AssertSqlConfig(expectedSqlConfig, actualSqlConfig);
                }

                await mgr.DeleteAll();
                actualSqlConfigs = await mgr.GetAll();
                Assert.AreEqual(0, actualSqlConfigs.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #region Helper

        private void AssertSqlConfig(SqlConfig expectedSqlConfig, SqlConfig actualSqlConfig)
        {
            Assert.AreEqual(expectedSqlConfig.Id, actualSqlConfig.Id);
            Assert.AreEqual(expectedSqlConfig.DatabaseId, actualSqlConfig.DatabaseId);
            Assert.AreEqual(expectedSqlConfig.Query, actualSqlConfig.Query);

            var expectedSqlVariableConfigs = expectedSqlConfig.SqlVariableConfigs.ToList();
            var actualSqlVariableConfigs = actualSqlConfig.SqlVariableConfigs.ToList();

            Assert.AreEqual(expectedSqlVariableConfigs.Count, actualSqlVariableConfigs.Count);
            foreach (var expectedSqlVariableConfig in expectedSqlVariableConfigs)
            {
                var actualSqlVariableConfig = actualSqlVariableConfigs.First(x => x.Name == expectedSqlVariableConfig.Name);
                Assert.AreEqual(expectedSqlVariableConfig.SqlConfigId, actualSqlVariableConfig.SqlConfigId);
            }
        }

        #endregion
    }
}