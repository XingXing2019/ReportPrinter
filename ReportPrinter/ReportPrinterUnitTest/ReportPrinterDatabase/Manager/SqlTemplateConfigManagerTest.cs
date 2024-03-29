﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class SqlTemplateConfigManagerTest : DatabaseTestBase<SqlTemplateConfigModel>
    {
        private readonly SqlConfigEFCoreManager _sqlConfigMgr;

        public SqlTemplateConfigManagerTest()
        {
            Manager = new SqlTemplateConfigEFCoreManager();
            _sqlConfigMgr = new SqlConfigEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            _sqlConfigMgr.DeleteAll().Wait();
            Manager.DeleteAll().Wait();
        }

        [Test]
        [TestCase(typeof(SqlTemplateConfigSPManager))]
        [TestCase(typeof(SqlTemplateConfigEFCoreManager))]
        public async Task TestSqlTemplateConfigManager_Get(Type managerType)
        {
            try
            {
                var mgr = (ISqlTemplateConfigManager)Activator.CreateInstance(managerType);

                var sqlConfigs1 = new List<SqlConfig>();
                var sqlConfigs2 = new List<SqlConfig>();

                for (int i = 0; i < 10; i++)
                {
                    var sqlConfigId = Guid.NewGuid();
                    var id = $"Test Sql Config {i + 1}";
                    var databaseId = $"Test DB {i + 1}";
                    var query = $"Test Query {i + 1}";
                    var sqlVariableNames = new List<string> { $"Variable {i + 1}" };

                    var sqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                    await _sqlConfigMgr.Post(sqlConfig);

                    if (i % 2 == 0)
                        sqlConfigs1.Add(sqlConfig);
                    else
                        sqlConfigs2.Add(sqlConfig);
                }

                var sqlTemplateConfigId = Guid.NewGuid();
                var sqlTemplateId = "Test Sql Template Config 1";
                var expectedSqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, sqlConfigs1, sqlTemplateId);

                await mgr.Post(expectedSqlTemplateConfig);

                var actualSqlTemplateConfig = await mgr.Get(sqlTemplateConfigId);
                Assert.NotNull(actualSqlTemplateConfig);
                AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);

                sqlTemplateId = "Test Sql Template Config 2";
                expectedSqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, sqlConfigs2, sqlTemplateId);
                await mgr.PutSqlTemplateConfig(expectedSqlTemplateConfig);

                actualSqlTemplateConfig = await mgr.Get(sqlTemplateConfigId);
                Assert.NotNull(actualSqlTemplateConfig);
                AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);

                await mgr.Delete(sqlTemplateConfigId);
                actualSqlTemplateConfig = await mgr.Get(sqlTemplateConfigId);
                Assert.IsNull(actualSqlTemplateConfig);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(SqlTemplateConfigSPManager))]
        [TestCase(typeof(SqlTemplateConfigEFCoreManager))]
        public async Task TestSqlTemplateConfigManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (ISqlTemplateConfigManager)Activator.CreateInstance(managerType);
                var sqlConfigs = new List<SqlConfig>();
                var expectedSqlTemplateConfigs = new List<SqlTemplateConfigModel>();
                var sqlTemplateConfigsToDelete = new List<Guid>();

                for (int i = 0; i < 10; i++)
                {
                    var sqlConfigId = Guid.NewGuid();
                    var id = $"Test Sql Config {i + 1}";
                    var databaseId = $"Test DB {i + 1}";
                    var query = $"Test Query {i + 1}";
                    var sqlVariableNames = new List<string> { $"Variable {i + 1}" };

                    var sqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                    await _sqlConfigMgr.Post(sqlConfig);
                    sqlConfigs.Add(sqlConfig);
                }

                for (int i = 0; i < 10; i++)
                {
                    var sqlTemplateConfigId = Guid.NewGuid();
                    var id = $"Test Sql Template Config {i + 1}";

                    var sqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, sqlConfigs, id);
                    await mgr.Post(sqlTemplateConfig);
                    expectedSqlTemplateConfigs.Add(sqlTemplateConfig);

                    if (i < 5)
                    {
                        sqlTemplateConfigsToDelete.Add(sqlTemplateConfigId);
                    }
                }

                var actualSqlTemplateConfigs = await mgr.GetAll();
                Assert.AreEqual(10, actualSqlTemplateConfigs.Count);
                foreach (var expectedSqlTemplateConfig in expectedSqlTemplateConfigs)
                {
                    var actualSqlTemplateConfig = actualSqlTemplateConfigs.Single(x => x.Id == expectedSqlTemplateConfig.Id);
                    AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);
                }

                await mgr.Delete(sqlTemplateConfigsToDelete);
                actualSqlTemplateConfigs = await mgr.GetAll();

                Assert.AreEqual(5, actualSqlTemplateConfigs.Count);
                expectedSqlTemplateConfigs = expectedSqlTemplateConfigs.Where(x => !sqlTemplateConfigsToDelete.Contains(x.SqlTemplateConfigId)).ToList();

                foreach (var expectedSqlTemplateConfig in expectedSqlTemplateConfigs)
                {
                    var actualSqlTemplateConfig = actualSqlTemplateConfigs.Single(x => x.Id == expectedSqlTemplateConfig.Id);
                    AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);
                }

                await mgr.DeleteAll();
                actualSqlTemplateConfigs = await mgr.GetAll();
                Assert.AreEqual(0, actualSqlTemplateConfigs.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(SqlTemplateConfigSPManager))]
        [TestCase(typeof(SqlTemplateConfigEFCoreManager))]
        public async Task TestSqlConfigManager_GetAllBySqlTemplateIdPrefix(Type managerType)
        {
            try
            {
                var mgr = (ISqlTemplateConfigManager)Activator.CreateInstance(managerType);
                var sqlConfigs = new List<SqlConfig>();

                for (int i = 0; i < 10; i++)
                {
                    var sqlConfigId = Guid.NewGuid();
                    var id = $"Test Sql Config {i + 1}";
                    var databaseId = $"Test DB {i + 1}";
                    var query = $"Test Query {i + 1}";
                    var sqlVariableNames = new List<string> { $"Variable {i + 1}" };

                    var sqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                    await _sqlConfigMgr.Post(sqlConfig);
                    sqlConfigs.Add(sqlConfig);
                }

                var expectedTestDbSqlTemplateConfigs = new List<SqlTemplateConfigModel>();
                var expectedRealDbSqlTemplateConfigs = new List<SqlTemplateConfigModel>();

                for (int i = 0; i < 10; i++)
                {
                    var sqlTemplateConfigId = Guid.NewGuid();
                    var id = $"Test Sql Template Config {i + 1}";

                    var sqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, sqlConfigs, id);
                    await mgr.Post(sqlTemplateConfig);
                    expectedTestDbSqlTemplateConfigs.Add(sqlTemplateConfig);
                }

                for (int i = 0; i < 5; i++)
                {
                    var sqlTemplateConfigId = Guid.NewGuid();
                    var id = $"Real Sql Template Config {i + 1}";
                    var sqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, sqlConfigs, id);
                    await mgr.Post(sqlTemplateConfig);
                    expectedRealDbSqlTemplateConfigs.Add(sqlTemplateConfig);
                }

                var templateIdPrefix = "Test Sql Template Config";
                var actualSqlTemplateConfigs = await mgr.GetAllBySqlTemplateIdPrefix(templateIdPrefix);
                Assert.AreEqual(10, actualSqlTemplateConfigs.Count);

                foreach (var expectedSqlTemplateConfig in expectedTestDbSqlTemplateConfigs)
                {
                    var actualSqlTemplateConfig = actualSqlTemplateConfigs.Single(x => x.Id == expectedSqlTemplateConfig.Id);
                    AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);
                }

                templateIdPrefix = "Real Sql Template Config";
                actualSqlTemplateConfigs = await mgr.GetAllBySqlTemplateIdPrefix(templateIdPrefix);
                Assert.AreEqual(5, actualSqlTemplateConfigs.Count);

                foreach (var expectedSqlTemplateConfig in expectedRealDbSqlTemplateConfigs)
                {
                    var actualSqlTemplateConfig = actualSqlTemplateConfigs.Single(x => x.Id == expectedSqlTemplateConfig.Id);
                    AssertHelper.AssertSqlTemplateConfig(expectedSqlTemplateConfig, actualSqlTemplateConfig);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}