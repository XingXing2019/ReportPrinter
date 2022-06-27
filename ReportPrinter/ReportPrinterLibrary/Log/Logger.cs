using System.Text.Json;
using NLog;

namespace ReportPrinterLibrary.Log
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, string procName)
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Info(message);
        }

        public static void Debug(string message, string procName)
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Debug(message);
        }

        public static void Warn(string message, string procName)
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Warn(message);
        }

        public static void Error(string message, string procName)
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            _logger.Error(message);
        }

        public static void LogJson(string message, object obj, string procName)
        {
            message = string.IsNullOrEmpty(procName) ? message : $"{procName}: {message}";
            var option = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(obj, option);

            message = $"{message}\n{json}";
            _logger.Debug(message);
        }
    }
}