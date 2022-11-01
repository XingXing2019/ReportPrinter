using System;
using System.IO;

namespace ReportPrinterLibrary.Code.Config.Helper
{
    public class ConfigPath
    {
        private const string S_FILE_NAME = "Config.xml";
        public string GetConfigPath()
        {
            var currentLocation = this.GetType().Assembly.Location;
            var directory = Path.GetDirectoryName(currentLocation);

            var path = Path.Combine(directory, S_FILE_NAME);
            return path;
        }

        public string GetAppConfigPath()
        {
            var currentLocation = this.GetType().Assembly.Location;
            var directory = Path.GetDirectoryName(currentLocation);
            var appName = AppDomain.CurrentDomain.FriendlyName;

            var path = Path.Combine(directory, "Config", $"{appName}.{S_FILE_NAME}");
            return path;
        }
    }
}