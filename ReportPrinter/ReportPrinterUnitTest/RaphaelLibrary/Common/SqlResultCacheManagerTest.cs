using NUnit.Framework;
using RaphaelLibrary.Code.Common;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;
using System.Collections.Generic;
using System;
using System.Data;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class SqlResultCacheManagerTest : TestBase
    {
        private readonly string _fieldName = "_cache";


        #region Helper

        private Dictionary<Guid, Dictionary<string, DataTable>> GetSqlVariableRepo()
        {
            var type = typeof(SqlVariableManager);
            return GetPrivateField<Dictionary<Guid, Dictionary<string, DataTable>>>(type, _fieldName, SqlResultCacheManager.Instance);
        }

        #endregion
    }
}