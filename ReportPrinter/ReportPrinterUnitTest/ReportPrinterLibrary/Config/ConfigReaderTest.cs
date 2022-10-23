using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Config.Helper;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.Config
{
    public class ConfigReaderTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestReadConfig(bool expectedRes)
        {
            var configPath = expectedRes ? @".\ReportPrinterLibrary\Config\TestFile\ValidConfig.xml" : "InvalidPath";

            try
            {
                var actualConfig = ConfigReader<AppConfig>.ReadConfig(configPath);
                Assert.IsNotNull(actualConfig);
                
                var expectedConfig = (AppConfig)Activator.CreateInstance(typeof(AppConfig), true);
                Assert.IsNotNull(expectedConfig);

                expectedConfig.RabbitMQConfig = new RabbitMQConfig
                {
                    UserName = "TestUserName",
                    Password = "TestPassword",
                    Host = "TestHost",
                    VirtualHost = "TestVirtualHost"
                };
                expectedConfig.DatabaseConfigList = new List<DatabaseConfig>
                {
                    new DatabaseConfig { Id = "TestDB1", DatabaseName = "TestDB1", ConnectionString = "TestConnectionString1" },
                    new DatabaseConfig { Id = "TestDB2", DatabaseName = "TestDB2", ConnectionString = "TestConnectionString2" },
                    new DatabaseConfig { Id = "TestDB3", DatabaseName = "TestDB3", ConnectionString = "TestConnectionString3" },
                };
                expectedConfig.TargetDatabase = "TestDB1";
                expectedConfig.ServicePathConfigList = new List<ServicePathConfig>
                {
                    new ServicePathConfig { Id = "TestService", Path = "TestServicePath" }
                };

                AssertObject(expectedConfig, actualConfig);
            }
            catch (IOException ex)
            {
                var expectedError = $"{configPath} does not exists";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}