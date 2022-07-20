using System;
using System.Threading;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Print
{
    public abstract class PrinterBase
    {
        public bool PrintReport(string fileName, string filePath, string printerId, int numberOfCopy, int timeout)
        {
            var procName = $"{this.GetType().Name}.{nameof(PrintReport)}";

            try
            {
                Logger.Info($"Start print file: {filePath} at printer: {printerId}", procName);
                var tSendToPrinter = new Thread(() => SendToPrinter(fileName, filePath, printerId, numberOfCopy));
                tSendToPrinter.Start();
                var isSuccess = tSendToPrinter.Join(timeout);

                if (isSuccess)
                {
                    Logger.Info($"Success to print file: {filePath} at printer: {printerId}", procName);
                    return true;
                }
                else
                {
                    Logger.Warn($"Print process reach timeout: {timeout}, force to stop process", procName);
                    tSendToPrinter.Abort();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during print file: {filePath} at printer: {printerId}. Ex: {ex.Message}", procName);
                return true;
            }

        }

        protected abstract void SendToPrinter(string fileName, string filePath, string printerId, int numberOfCopy);
    }
}