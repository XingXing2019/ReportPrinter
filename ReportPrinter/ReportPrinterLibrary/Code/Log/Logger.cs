using System.Text.Json;
using System.Threading;
using System.Xml;
using NLog;

namespace ReportPrinterLibrary.Code.Log
{
    public static class Logger
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        private static readonly string _missingXmlElement = "XML is incorrect. Unable to locate xml element: {0}.";
        private static readonly string _setDefaultValue = "{0} is not provide or incorrectly provided. Initialize {0} to {1}";

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

        public static string GenerateMissingXmlLog(string missingXml, XmlNode node)
        {
            return string.Format(_missingXmlElement, $"{GenerateAncestorPath(node)}->{missingXml}");
        }

        public static string GenerateDefaultValue(string name, string value)
        {
            return string.Format(_setDefaultValue, name, value);
        }

        #region Helper

        private static string FormatMessage(string message, string procName)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            message = string.IsNullOrEmpty(procName) ? message : $"{procName} | {message}";
            return $" Thread:{threadId} | {message}";
        }

        private static string GenerateAncestorPath(XmlNode node)
        {
            if (node?.ParentNode == null)
                return string.Empty;
            var parent = GenerateAncestorPath(node.ParentNode);
            return string.IsNullOrEmpty(parent) ? node.Name : $"{parent}->{node.Name}";
        }

        #endregion
    }
}