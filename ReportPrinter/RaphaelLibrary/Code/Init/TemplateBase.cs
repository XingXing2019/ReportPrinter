using System.Linq;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Print;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace RaphaelLibrary.Code.Init
{
    public abstract class TemplateBase
    {
        public abstract TemplateBase Clone();
        public abstract bool TryCreateReport(IPrintReport message);

        protected void StoreSqlVariables(IPrintReport message)
        {
            var sqlVariables = message.SqlVariables.ToDictionary(x => x.Name, x => new SqlVariable { Name = x.Name, Value = x.Value });

            var sqlVariableManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            var sqlVariableManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(sqlVariableManagerType);
            sqlVariableManager.StoreSqlVariables(message.MessageId, sqlVariables);
        }

        protected bool PrintReport(IPrintReport message, string fileName, string filePath, int timeout)
        {
            var printer = PrinterFactory.CreatePrinter(message.ReportType);
            return printer.PrintReport(fileName, filePath, message.PrinterId, message.NumberOfCopy, timeout);
        }
    }
}