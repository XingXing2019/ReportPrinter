namespace RaphaelLibrary.Code.Print
{
    public class PdfPrinter : PrinterBase
    {
        protected override void SendToPrinter(string filePath, string printerId, int numberOfCopy)
        {
            for (int i = 0; i < numberOfCopy; i++)
            {
                Print(filePath, printerId);
            }
        }


		#region Helper

		private void Print(string filePath, string printerName)
        {
            
        }

		#endregion
	}
}