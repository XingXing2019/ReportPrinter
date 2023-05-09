using System;
using NUnit.Framework;
using System.IO;
using ReportPrinterLibrary.Code.Helper;

namespace ReportPrinterUnitTest.ReportPrinterLibrary.Config
{
    public class ConfigPathTest
    {
        private readonly string _directory;
        public ConfigPathTest()
        {
            var currentLocation = this.GetType().Assembly.Location;
            _directory = Path.GetDirectoryName(currentLocation);
        }

        [Test]
        public void TestGetConfigPath()
        {
            try
            {
                var expectedPath = Path.Combine(_directory, "Config.xml");
                var actualPath = new ConfigPath().GetConfigPath();
                Assert.AreEqual(expectedPath, actualPath);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestGetAppConfigPath()
        {
            try
            {
                var expectedPath = Path.Combine(_directory, "Config", "ReSharperTestRunner.Config.xml");
                var actualPath = new ConfigPath().GetAppConfigPath();

                Assert.AreEqual(expectedPath, actualPath);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}