using System.IO;
using System.Xml.Serialization;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterLibrary.Code.Config.Helper
{
    public static class ConfigReader<T>
    {
        public static T ReadConfig(string configPath)
        {
            var procName = $"ConfigReader.{nameof(ReadConfig)}";

            if (!File.Exists(configPath))
            {
                var error = $"{configPath} does not exists";
                Logger.Error(error, procName);
                throw new IOException(error);
            }

            Logger.Debug($"Start reading config at {configPath}", procName);
            var serializer = new XmlSerializer(typeof(T));
            using var stream = new StreamReader(configPath);
            var config = (T)serializer.Deserialize(stream);
            Logger.Debug($"Get config of {typeof(T)}", procName);

            return config;
        }
    }
}