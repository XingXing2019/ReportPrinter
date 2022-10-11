using ReportPrinterDatabase.Code.Manager;

namespace ReportPrinterUnitTest.Database
{
    public class DatabaseTestBase<T> : TestBase
    {
        protected IManager<T> Manager;
    }
}