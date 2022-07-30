using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RaphaelLibrary.Code.Print
{
    public class LabelPrinter : PrinterBase
    {
        protected override void SendToPrinter(string fileName, string filePath, string printerId, int numberOfCopy)
        {
            for (int i = 0; i < numberOfCopy; i++)
            {
                SendFileToPrinter(filePath, printerId);
            }
        }


		#region Helper

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private class DOCINFOA
		{
			[MarshalAs(UnmanagedType.LPStr)]
			public string _docName;
			[MarshalAs(UnmanagedType.LPStr)]
			public string _outputFile;
			[MarshalAs(UnmanagedType.LPStr)]
			public string _dataType;
		}

		[DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

		[DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool ClosePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

		[DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool EndDocPrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool StartPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool EndPagePrinter(IntPtr hPrinter);

		[DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

		private void SendBytesToPrinter(string printerId, IntPtr bytes, int length)
		{
			var di = new DOCINFOA { _docName = "RAW Document", _dataType = "RAW" };
			
            if (OpenPrinter(printerId.Normalize(), out var printer, IntPtr.Zero))
			{
				if (StartDocPrinter(printer, 1, di))
				{
					if (StartPagePrinter(printer))
					{
						WritePrinter(printer, bytes, length, out _);
						EndPagePrinter(printer);
					}
					EndDocPrinter(printer);
				}
				ClosePrinter(printer);
			}
		}

		private void SendFileToPrinter(string filePath, string printerId)
		{
			var fileStream = new FileStream(filePath, FileMode.Open);
			var binaryReader = new BinaryReader(fileStream);

			var length = Convert.ToInt32(fileStream.Length);
			var bytes = binaryReader.ReadBytes(length);
			var pUnmanagedBytes = Marshal.AllocCoTaskMem(length);
			Marshal.Copy(bytes, 0, pUnmanagedBytes, length);
			SendBytesToPrinter(printerId, pUnmanagedBytes, length);
			Marshal.FreeCoTaskMem(pUnmanagedBytes);
			fileStream.Close();
			fileStream.Dispose();
		}

		#endregion
	}
}