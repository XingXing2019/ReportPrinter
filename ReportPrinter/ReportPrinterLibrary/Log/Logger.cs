using System.Text.Json;
using System.Threading;
using NLog;

namespace ReportPrinterLibrary.Log
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, string procName)
        {
            _logger.Info(FormatMessage(message, procName));
        }

        public static void Debug(string message, string procName)
        {
            _logger.Debug(FormatMessage(message, procName));
        }

        public static void Warn(string message, string procName)
        {
            _logger.Warn(FormatMessage(message, procName));
        }

        public static void Error(string message, string procName)
        {
            _logger.Error(FormatMessage(message, procName));
        }

        public static void LogJson(string message, object obj, string procName)
        {
            message = FormatMessage(message, procName);
            var option = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(obj, option);

            message = $"{message}\n{json}";
            _logger.Debug(message);
        }


        #region Helper

        private static string FormatMessage(string message, string procName)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            message = string.IsNullOrEmpty(procName) ? message : $"{procName} | {message}";
            return $" Thread:{threadId} | {message}";
        }

        #endregion
    }
}