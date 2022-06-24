using NLog;

namespace ReportPrinterLibrary.Log
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, string procName = "")
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Info(message);
        }

        public static void Debug(string message, string procName = "")
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Debug(message);
        }

        public static void Error(string message, string procName = "")
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Error(message);
        }
    }
}