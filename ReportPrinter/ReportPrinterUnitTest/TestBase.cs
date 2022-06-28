using System.Collections.Generic;
using System.Linq;
using ReportPrinterDatabase.Manager;
using ReportPrinterLibrary.Config.Configuration;

namespace ReportPrinterUnitTest
{
    public abstract class TestBase<T>
    {
        protected IManager<T> Manager;
        protected readonly Dictionary<string, string> ServicePath;

        protected TestBase()
        {
            var servicePathList = AppConfig.Instance.ServicePathConfigList;
            ServicePath = servicePathList.ToDictionary(x => x.Id, x => x.Path);
        }
    }
}