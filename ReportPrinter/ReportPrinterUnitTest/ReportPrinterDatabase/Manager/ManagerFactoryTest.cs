using NUnit.Framework;
using System;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class ManagerFactoryTest
    {
        [Test]
        [TestCase(DatabaseManagerType.EFCore, typeof(ISqlConfigManager), typeof(SqlConfigEFCoreManager))]
        [TestCase(DatabaseManagerType.SP, typeof(ISqlConfigManager), typeof(SqlConfigSPManager))]
        [TestCase(DatabaseManagerType.EFCore, typeof(IPrintReportMessageManager<IPrintReport>), typeof(PrintReportMessageEFCoreManager))]
        [TestCase(DatabaseManagerType.SP, typeof(IPrintReportMessageManager<IPrintReport>), typeof(PrintReportMessageSPManager))]
        public void TestCreateManager(DatabaseManagerType mgrType, Type managerType, Type expectedType)
        {
            try
            {
                if (managerType == typeof(ISqlConfigManager))
                {
                    var manager = ManagerFactory.CreateManager<SqlConfig>(managerType, mgrType);
                    Assert.AreEqual(expectedType, manager.GetType());
                }
                else
                {
                    var manager = ManagerFactory.CreateManager<IPrintReport>(managerType, mgrType);
                    Assert.AreEqual(expectedType, manager.GetType());
                }
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid type: {managerType} for manager";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}