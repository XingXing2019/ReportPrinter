using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

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
        public async Task TestSqlConfigManager_Get(Type managerType)
        {
            try
            {
                var mgr = (ISqlConfigManager)Activator.CreateInstance(managerType);
                var expectedSqlConfig = CreateSqlConfig("Test SQL Config");
                await mgr.Post(expectedSqlConfig);

                var actualSqlConfig = await mgr.Get(expectedSqlConfig.SqlConfigId);
                Assert.NotNull(actualSqlConfig);
                AssertSqlConfig(expectedSqlConfig, actualSqlConfig);

                await mgr.Delete(expectedSqlConfig.SqlConfigId);
                actualSqlConfig = await mgr.Get(expectedSqlConfig.SqlConfigId);
                Assert.IsNull(actualSqlConfig);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(typeof(SqlConfigEFCoreManager))]
        public async Task TestSqlConfigManager_GetAll(Type managerType)
        {
            try
            {
                var mgr = (ISqlConfigManager)Activator.CreateInstance(managerType);
                var expectedSqlConfigs = new List<SqlConfig>();

                for (int i = 0; i < 10; i++)
                {
                    var expectedSqlConfig = CreateSqlConfig($"Test SQL Config {i + 1}");
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
                var actualSqlVariableConfig = actualSqlVariableConfigs.First(x => x.SqlVariableConfigId == expectedSqlVariableConfig.SqlVariableConfigId);
                Assert.AreEqual(expectedSqlVariableConfig.SqlConfigId, actualSqlVariableConfig.SqlConfigId);
                Assert.AreEqual(expectedSqlVariableConfig.Name, actualSqlVariableConfig.Name);
            }
        }

        #endregion
    }
}