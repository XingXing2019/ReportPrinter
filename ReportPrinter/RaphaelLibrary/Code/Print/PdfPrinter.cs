using RawPrint;

namespace RaphaelLibrary.Code.Print
{
    public class PdfPrinter : PrinterBase
    {
        protected override void SendToPrinter(string fileName, string filePath, string printerId, int numberOfCopy)
        {
            for (int i = 0; i < numberOfCopy; i++)
            {
                Print(fileName, filePath, printerId);
            }
        }


		#region Helper

		private void Print(string fileName, string filePath, string printerId)
        {
            IPrinter printer = new Printer();
            printer.PrintRawFile(printerId, filePath, fileName);
        }

		#endregion
	}
}