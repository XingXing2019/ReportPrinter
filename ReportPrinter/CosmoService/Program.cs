using System;
using System.Windows.Forms;
using CosmoService.Code.Forms;
using ReportPrinterLibrary.Code.Log;

namespace CosmoService
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var procName = $"RaphaelService.{nameof(Main)}";

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            Logger.Info($"CosmoService start running", procName);
        }
    }
}
