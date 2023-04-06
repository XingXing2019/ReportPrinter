using System;
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
        [TestCase(typeof(SqlConfigSPManager))]
        [TestCase(typeof(SqlConfigEFCoreManager))]
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
                AssertHelper.AssertSqlConfig(expectedSqlConfig, actualSqlConfig);

                id = "Test Sql Config 2";
                databaseId = "Test DB 2";
                query = "Test Query 2";
                sqlVariableNames = new List<string> { "Variable A", "Variable B", "Variable C" };
                expectedSqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);

                await mgr.PutSqlConfig(expectedSqlConfig);

                actualSqlConfig = await mgr.Get(sqlConfigId);
                Assert.NotNull(actualSqlConfig);
                AssertHelper.AssertSqlConfig(expectedSqlConfig, actualSqlConfig);

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
        [TestCase(typeof(SqlConfigSPManager))]
        [TestCase(typeof(SqlConfigEFCoreManager))]
        public async Task TestSqlConfigManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (ISqlConfigManager)Activator.CreateInstance(managerType);
                var expectedSqlConfigs = new List<SqlConfig>();
                var sqlConfigsToDelete = new List<Guid>();

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

                    if (i < 5)
                    {
                        sqlConfigsToDelete.Add(sqlConfigId);
                    }
                }
                
                var actualSqlConfigs = await mgr.GetAll();
                Assert.AreEqual(10, actualSqlConfigs.Count);

                foreach (var expectedSqlConfig in expectedSqlConfigs)
                {
                    var actualSqlConfig = actualSqlConfigs.Single(x => x.SqlConfigId == expectedSqlConfig.SqlConfigId);
                    AssertHelper.AssertSqlConfig(expectedSqlConfig, actualSqlConfig);
                }

                await mgr.Delete(sqlConfigsToDelete);
                actualSqlConfigs = await mgr.GetAll();

                Assert.AreEqual(5, actualSqlConfigs.Count);
                expectedSqlConfigs = expectedSqlConfigs.Where(x => !sqlConfigsToDelete.Contains(x.SqlConfigId)).ToList();
                
                foreach (var expectedSqlConfig in expectedSqlConfigs)
                {
                    var actualSqlConfig = actualSqlConfigs.Single(x => x.SqlConfigId == expectedSqlConfig.SqlConfigId);
                    AssertHelper.AssertSqlConfig(expectedSqlConfig, actualSqlConfig);
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

        

        #endregion
    }
}