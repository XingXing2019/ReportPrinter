namespace ReportPrinterLibrary.Code.Config.Configuration
{
    public class RedisConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public double AbsoluteExpirationRelativeToNow { get; set; }
    }
}