using System;
using System.Collections.Generic;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Database;

namespace ReportPrinterUnitTest.Database
{
    public class DatabaseConnectionListTest : TestBase
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestTryGetDatabaseConnection(bool expectedRes)
        {
            var databaseConnections = new Dictionary<string, DatabaseConnection>();
            var id = "TestId";
            var dbName = "TestDb";
            var connectionString = "TestConnectionString";

            if (expectedRes)
            {
                var dbConnection = new DatabaseConnection(id, dbName, connectionString);
                databaseConnections.Add(id, dbConnection);
            }

            var dbConnectionList = new DatabaseConnectionList(databaseConnections);

            try
            {
                var actualRes = dbConnectionList.TryGetDatabaseConnection(id, out var dbConnection);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(dbName, dbConnection.DatabaseName);
                    Assert.AreEqual(connectionString, dbConnection.ConnectionString);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}