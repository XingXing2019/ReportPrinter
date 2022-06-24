using System.IO;
using System.Xml.Serialization;
using ReportPrinterLibrary.Log;

namespace ReportPrinterLibrary.Configuration
{
    public class ConfigReader<T>
    {
        public T ReadConfig()
        {
            var configPath = new ConfigPath().GetConfigPath();

            if (!File.Exists(configPath))
            {
                var error = $"{configPath} does not exists";
                Logger.Error(error, nameof(ReadConfig));
                throw new IOException(error);
            }

            var serializer = new XmlSerializer(typeof(T));
            using var stream = new StreamReader(configPath);
            var config = (T)serializer.Deserialize(stream);

            return config;
        }
    }
}