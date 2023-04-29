using System.Collections.Generic;
using MassTransit.Internals.Reflection;
using ReportPrinterLibrary.Code.Config.Helper;
using ReportPrinterLibrary.Code.Enum;

namespace ReportPrinterLibrary.Code.Config.Configuration
{
    public class AppConfig
    {
        public RabbitMQConfig RabbitMQConfig { get; set; }
        public RedisConfig RedisConfig { get; set; }
        public List<DatabaseConfig> DatabaseConfigList { get; set; }
        public string TargetDatabase { get; set; }
        public CacheManagerType SqlResultCacheManagerType { get; set; }
        public CacheManagerType SqlVariableCacheManagerType { get; set; }
        public DatabaseManagerType DatabaseManagerType { get; set; }
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
                            var configPath = new ConfigPath().GetConfigPath();
                            _instance = ConfigReader<AppConfig>.ReadConfig(configPath);
                        }
                    }
                }

                return _instance;
            }
        }

        private AppConfig() { }
    }
}