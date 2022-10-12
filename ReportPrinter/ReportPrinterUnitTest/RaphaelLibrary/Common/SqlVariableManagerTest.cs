using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class SqlVariableManagerTest : TestBase
    {
        private readonly string _fieldName = "_sqlVariableRepo";

        [Test]
        public void TestStoreSqlVariables()
        {

        }


        #region Helper

        private Dictionary<Guid, Dictionary<string, SqlVariable>> GetSqlVariableRepo()
        {
            var type = typeof(SqlVariableManager);
            return GetPrivateField<Dictionary<Guid, Dictionary<string, SqlVariable>>>(type, _fieldName, SqlVariableManager.Instance);
        }

        #endregion
    }
}