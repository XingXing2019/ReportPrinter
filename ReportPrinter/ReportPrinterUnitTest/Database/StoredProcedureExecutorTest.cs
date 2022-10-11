using NUnit.Framework;
using ReportPrinterDatabase.Code.Executor;
using System.Threading.Tasks;
using System;

namespace ReportPrinterUnitTest.Database
{
    public class StoredProcedureExecutorTest
    {
        private readonly StoredProcedureExecutor _executor;
        public StoredProcedureExecutorTest()
        {
            _executor = new StoredProcedureExecutor();
        }


        [Test]
        public async Task TestExecuteNonQueryAsync()
        {
        }
    }
}