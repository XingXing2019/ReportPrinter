using System.IO;

namespace ReportPrinterLibrary.Config.Helper
{
    public class ConfigPath
    {
        public string GetConfigPath()
        {
            var currentLocation = this.GetType().Assembly.Location;
            var directory = Path.GetDirectoryName(currentLocation);
            return $"{directory}\\Config.xml";
        }
    }
}