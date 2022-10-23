using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Database;

namespace ReportPrinterUnitTest.Database
{
    public class DatabaseManagerTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetConnectionString(bool expectedRes)
        {
            var id = "TestId";
            var dbName = "TestDb";
            var expectedConnStr = "TestConnectionString";
            var dbConnection = new DatabaseConnection(id, dbName, expectedConnStr);
            var databaseConnections = new Dictionary<string, DatabaseConnection>();

            if (expectedRes)
            {
                databaseConnections.Add(id, dbConnection);
            }

            var dummyDbConnectionList = new DatabaseConnectionList(databaseConnections);
            SetPrivateField<DatabaseConnectionList>(DatabaseManager.Instance, "_databaseConnectionList", dummyDbConnectionList);
            
            try
            {
                var actualRes = DatabaseManager.Instance.TryGetConnectionString(id, out var actualConnStr);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(expectedConnStr, actualConnStr);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}