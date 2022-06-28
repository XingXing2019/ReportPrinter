using System.Collections.Generic;
using ReportPrinterLibrary.Config.Helper;

namespace ReportPrinterLibrary.Config.Configuration
{
    public class AppConfig
    {
        public RabbitMQConfig RabbitMQConfig { get; set; }
        public List<DatabaseConfig> DatabaseConfigList { get; set; }
        public string TargetDatabase { get; set; }
        public List<ServicePathConfig> ServicePathConfigList { get; set; }

        private static AppConfig _instance;
        private static readonly object _lock = new object();

        public static AppConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = ConfigReader<AppConfig>.ReadConfig();
                        }
                    }
                }

                return _instance;
            }
        }

        private AppConfig() { }
    }
}