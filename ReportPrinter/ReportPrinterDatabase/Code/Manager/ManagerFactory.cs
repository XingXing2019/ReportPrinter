using System;
using System.Linq;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterDatabase.Code.Manager
{
    public class ManagerFactory
    {
        public static IManager<T> CreateManager<T>(Type type, DatabaseManagerType managerType)
        {
            var procName = $"ManagerFactory.{nameof(CreateManager)}";
            
            if (managerType == DatabaseManagerType.EFCore || managerType == DatabaseManagerType.SP)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var mgrType = assemblies.SelectMany(x => x.GetTypes())
                    .FirstOrDefault(x => type.IsAssignableFrom(x) && x.FullName.Contains(managerType.ToString()));

                var instance = Activator.CreateInstance(mgrType);
                return (IManager<T>) instance;
            }
            else
            {
                var error = $"Invalid type: {managerType} for manager";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }
        }
    }
}